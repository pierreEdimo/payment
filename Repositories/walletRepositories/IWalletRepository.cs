using Microsoft.AspNetCore.Mvc;
using payment.Dto;

namespace payment.Repositories.WalletRepositories;

public interface IWalletRepository
{
    Task<ActionResult<List<WalletDto>>> GetAllWallets();

    Task<ActionResult<WalletDto>> GetWalletByUserId(string uid);

    Task<ActionResult<WalletDto>> GetWalletByUniqueWalletId(string uwid);

    Task<IActionResult> ChargeWallet(string uwid, ChargeWalletDto newCharge);

    Task<IActionResult> TransferCredit(string uwid, string receiverUwid, TransferCreditDto newTransfer);

    Task<ActionResult<WalletDto>> CreateUserWallet(CreateWalletDto newWalletDto);

    Task<IActionResult> HardDeleteWallet(string uwid);

    Task<IActionResult> SoftDeleteWallet(string uwid);

    Task<ActionResult<List<WalletDto>>> GetDeletedWallets();

    Task<ActionResult<List<WalletDto>>> GetNonDeletedWallets();
}