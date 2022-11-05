using Microsoft.AspNetCore.Mvc;
using payment.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using payment.DatabaseContext;
using payment.model;

namespace payment.Repositories.WalletRepositories;

public class WalletRepository : IWalletRepository
{
    private readonly PayMentDbContext? _dbContext;
    private readonly IMapper? _mapper;

    public WalletRepository(IMapper mapper, PayMentDbContext dbContext)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ActionResult<List<WalletDto>>> GetAllWallets()
    {
        List<Wallet> wallets = await _dbContext!.Wallets!.ToListAsync();
        return _mapper!.Map<List<WalletDto>>(wallets);
    }

    public async Task<ActionResult<WalletDto>> GetWalletByUserId(string uid)
    {
        Wallet? wallet =
            await _dbContext!.Wallets!.FirstOrDefaultAsync(x => x.UserIdentifier == uid && x.IsDeleted == false);
        if (wallet == null)
            return new NotFoundResult();
        return _mapper!.Map<WalletDto>(wallet);
    }

    public async Task<ActionResult<WalletDto>> GetWalletByUniqueWalletId(string uwid)
    {
        Wallet? wallet =
            await _dbContext!.Wallets!.FirstOrDefaultAsync(x => x.UniqueWalletId == uwid && x.IsDeleted == false);
        if (wallet == null)
            return new NotFoundResult();
        return _mapper!.Map<WalletDto>(wallet);
    }

    public async Task<IActionResult> ChargeWallet(string uwid, ChargeWalletDto newCharge)
    {
        await using var transaction = _dbContext!.Database.BeginTransaction();

        Wallet? wallet =
            await _dbContext!.Wallets!.FirstOrDefaultAsync(x => x.UniqueWalletId == uwid && x.IsDeleted == false);
        if (wallet == null)
            return new NotFoundResult();

        try
        {
            wallet.Debit += Convert.ToDouble(newCharge.Amount);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            await transaction.RollbackAsync();
            throw;
        }

        return new NoContentResult();
    }

    public async Task<IActionResult> TransferCredit(string uwid, string receiverUwid, TransferCreditDto newTransfer)
    {
        await using var transaction = _dbContext!.Database.BeginTransaction();

        Wallet? wallet =
            await _dbContext!.Wallets!.FirstOrDefaultAsync(x => x.UniqueWalletId == uwid && x.IsDeleted == false);

        Wallet? receiverWallet =
            await _dbContext!.Wallets!.FirstOrDefaultAsync(
                x => x.UniqueWalletId == receiverUwid && x.IsDeleted == false);

        if (wallet == null || receiverWallet == null)
            return new NotFoundResult();

        if (newTransfer.Amount > wallet.Debit)
            return new ConflictResult();

        try
        {
            wallet.Debit -= Convert.ToDouble(newTransfer.Amount);
            receiverWallet.Debit += Convert.ToDouble(newTransfer.Amount);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            await transaction.RollbackAsync();
            throw;
        }

        return new NoContentResult();
    }

    public async Task<ActionResult<WalletDto>> CreateUserWallet(CreateWalletDto newWalletDto)
    {
        bool exists = await _dbContext!.Wallets!.AnyAsync(x => x.UserIdentifier == newWalletDto.UserIdentifier);
        if (exists) return new ConflictResult();

        bool created = false;
        Wallet? newWallet;
        WalletDto? walletDto = new WalletDto();

        while (!created)
        {
            newWallet = _mapper!.Map<Wallet>(newWalletDto);
            newWallet.UniqueWalletId =
                _createInialiseUniqueWalletId(newWallet.CompanyCode!,
                    newWallet.CountryCode!,
                    newWallet.UserIdentifier!);
            Wallet? existingWallet =
                await _dbContext!.Wallets!.FirstOrDefaultAsync(x => x.UniqueWalletId == newWallet.UniqueWalletId);

            if (existingWallet == null)
            {
                _dbContext.Add(newWallet);
                await _dbContext.SaveChangesAsync();
                walletDto = _mapper!.Map<WalletDto>(newWallet);
                Console.WriteLine(newWallet.Debit);
                created = true;
            }
        }

        return new CreatedResult("GetWalletByUniqueWalletId", walletDto);
    }

    public async Task<IActionResult> HardDeleteWallet(string uwid)
    {
        Wallet? wallet = await _dbContext!.Wallets!.FirstOrDefaultAsync(x => x.UniqueWalletId == uwid);
        if (wallet == null) return new NotFoundResult();
        _dbContext!.Wallets!.Remove(wallet);
        return new NoContentResult();
    }

    public async Task<IActionResult> SoftDeleteWallet(string uwid)
    {
        Wallet? wallet = await _dbContext!.Wallets!.FirstOrDefaultAsync(x => x.UniqueWalletId == uwid);
        if (wallet == null) return new NotFoundResult();
        wallet.IsDeleted = true;
        await _dbContext.SaveChangesAsync();

        return new NoContentResult();
    }

    public async Task<ActionResult<List<WalletDto>>> GetDeletedWallets()
    {
        List<Wallet> wallets = await _dbContext!.Wallets!.Where(x => x.IsDeleted == true).ToListAsync();
        return _mapper!.Map<List<WalletDto>>(wallets);
    }

    public async Task<ActionResult<List<WalletDto>>> GetNonDeletedWallets()
    {
        List<Wallet> wallets = await _dbContext!.Wallets!.Where(x => x.IsDeleted == false).ToListAsync();
        return _mapper!.Map<List<WalletDto>>(wallets);
    }

    private string _createInialiseUniqueWalletId(string companyCode,
        string countryCode,
        string userId)
    {
        string firstchars = userId.Substring(0, 3);
        Random rdRandom = new Random();
        int randNum = rdRandom.Next(100000000, 999999999);
        return countryCode.ToUpper() + companyCode.ToUpper() + firstchars.ToUpper() + randNum.ToString();
    }
}