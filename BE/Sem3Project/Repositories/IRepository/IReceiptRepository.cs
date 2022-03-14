using Sem3Project.Models.Dtos;

namespace Sem3Project.Repositories.IRepository
{
    public interface IReceiptRepository
    {
        bool CreateReceipt(ReceiptCreateDto receiptCreateDto);

        public bool Save();
    }
}
