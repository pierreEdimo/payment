using Microsoft.AspNetCore.Mvc;
using payment.Repositories.WalletRepositories;
using payment.Dto;

namespace payment.Controllers;

[ApiController]

public class WalletsController : ControllerBase
{
    private readonly IWalletRepository _repository;

    public WalletsController(IWalletRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// This Method get the List of all the Registered Wallets.
    /// </summary>
    /// <returns>List of Wallets as JSON</returns>
    /// <response code="200">OK</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<WalletDto>>> GetAllWallets()
    {
        return await _repository.GetAllWallets();
    }

    ///<summary>
    /// Get A single Wallet from the Database, based on the given UserId.
    ///</summary>
    ///<returns> A Single Wallet as JSON</returns>
    ///<response code="200">OK</response>
    ///<response code="404">Wallet was not found</response>
    [HttpGet("users/{uid}")]
    public async Task<ActionResult<WalletDto>> GetWalletByUserId(string uid)
    {
        return await _repository.GetWalletByUserId(uid);
    }

    ///<summary>
    /// Get A single Wallet from the Database, based on the given Uniquewalletid.
    ///</summary>
    ///<returns> A Single Wallet as JSON</returns>
    ///<response code="200">OK</response>
    ///<response code="404">Wallet was not found</response>
    [HttpGet("{uwid}")]
    public async Task<ActionResult<WalletDto>> GetWalletByUniqueId(string uwid)
    {
        return await _repository.GetWalletByUniqueWalletId(uwid);
    }

    ///<summary>
    /// Charge a credit to a specific account
    ///</summary>
    ///<returns> A Single Wallet as JSON</returns>
    ///<response code="204">No Content</response>
    ///<response code="404">Wallet was not found</response>
    [HttpPut("{uwid}")]
    public async Task<IActionResult> ChargeWallet(string uwid, [FromBody] ChargeWalletDto newCharge)
    {
        return await _repository.ChargeWallet(uwid, newCharge);
    }

    [HttpPut("from/{uwid}/to/{ruwid}")]
    public async Task<IActionResult> TransferCredit(string uwid, string ruwid,
        [FromBody] TransferCreditDto newTransfert)
    {
        return await _repository.TransferCredit(uwid, ruwid, newTransfert);
    }

    [HttpPost]
    public async Task<ActionResult<WalletDto>> CreateUserWallet(CreateWalletDto newWalletDto)
    {
        return await _repository.CreateUserWallet(newWalletDto);
    }

    [HttpDelete("hard/{uwid}")]
    public async Task<IActionResult> HardDeleteWallet(string uwid)
    {
        return await _repository.HardDeleteWallet(uwid);
    }

    [HttpDelete("soft/{uwid}")]
    public async Task<IActionResult> SoftDeleteWallet(string uwid)
    {
        return await _repository.SoftDeleteWallet(uwid);
    }

    [HttpGet("status/deleted")]
    public async Task<ActionResult<List<WalletDto>>> GetDeletedWallets()
    {
        return await _repository.GetDeletedWallets();
    }

    [HttpGet("status/existing")]
    public async Task<ActionResult<List<WalletDto>>> GetNonDeletedWallets()
    {
        return await _repository.GetNonDeletedWallets();
    }
}