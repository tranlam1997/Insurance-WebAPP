using Sem3Project.Data;
using Sem3Project.Models;
using Sem3Project.Models.Dtos;
using Sem3Project.Repositories.IRepository;
using System;

namespace Sem3Project.Repositories
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly ApplicationDbContext _db;

        public ReceiptRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateReceipt(ReceiptCreateDto receiptCreateDto)
        {
            var receipt = new Receipt();

            receipt.MinimumPayment = receiptCreateDto.MinimumPayment;
            receipt.Balance = receiptCreateDto.Balance;
            receipt.PaymentType = receiptCreateDto.PaymentType;
            receipt.CreatedDate = DateTime.Now;
            receipt.ModifiedDate = DateTime.Now;
            receipt.Status = "Pending";
            receipt.UserId = receiptCreateDto.UserId;
            receipt.InsuranceId = receiptCreateDto.InsuranceId;
            receipt.InsuranceType = receiptCreateDto.InsuranceType;
            receipt.PaymentId = receiptCreateDto.PaymentId;

            _db.Receipts.Add(receipt);
            return Save();
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }
    }
}
