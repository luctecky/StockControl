using StockControl.Domain.Entities;
using StockControl.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace StockControl.Application.Services
{
    public class RequisitionService
    {
        private readonly IRequisitionRepository _repository;

        public RequisitionService(IRequisitionRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Requisition> GetAllRequisitions()
        {
            return _repository.GetAll();
        }

        public Requisition GetRequisitionById(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Requisition> GetByPeriod(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new Exception("Start date cannot be greater than end date.");

            return _repository.GetByPeriod(startDate, endDate);
        }

        public void CreateRequisition(Requisition requisition)
        {
            ValidateRequisition(requisition);
            _repository.Add(requisition);
        }

        public void UpdateRequisition(Requisition requisition)
        {
            ValidateRequisition(requisition);
            _repository.Update(requisition);
        }

        public void DeleteRequisition(int id)
        {
            _repository.Delete(id);
        }

        private void ValidateRequisition(Requisition requisition)
        {
            if (string.IsNullOrWhiteSpace(requisition.ResponsibleEmployee))
                throw new Exception("Responsible employee name is required.");

            if (requisition.ResponsibleEmployee.Length < 3)
                throw new Exception("Employee name must be at least 3 characters.");

            if (requisition.WithdrawalDate == default)
                throw new Exception("Withdrawal date is required.");

            if (requisition.WithdrawalDate > DateTime.Today)
                throw new Exception("Withdrawal date cannot be in the future.");

            if (requisition.Items == null || requisition.Items.Count == 0)
                throw new Exception("Requisition must have at least one item.");

            foreach (var item in requisition.Items)
            {
                if (item.Quantity <= 0)
                    throw new Exception($"Quantity for '{item.Product.Name}' must be greater than zero.");
            }
        }
    }
}