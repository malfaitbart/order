using System.Collections.Generic;
using Order.Domain.Orders;

namespace Order.API.Controllers.Orders
{
	public class OrderReportDTO
	{
		public List<OrderDTO> Orders { get; private set; }
		public double ReportTotalPrice { get; private set; }

		public OrderReportDTO(List<OrderDTO> orders, double reportTotalPrice)
		{
			Orders = orders;
			ReportTotalPrice = reportTotalPrice;
		}
	}
}