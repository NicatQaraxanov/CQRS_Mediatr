using CQRS.Application.Features.Commands.Orders.AddOrder;
using CQRS.Application.Features.Commands.Orders.DeleteOrder;
using CQRS.Application.Features.Commands.Orders.EditOrder;
using CQRS.Application.Features.Queries.OrderItems.GetAllOrderItems;
using CQRS.Application.Features.Queries.Orders.GetAllOrders;
using CQRS.Application.Features.Queries.Orders.GetFilteredOrders;
using CQRS.Application.Features.Queries.Orders.GetOrder;
using CQRS.Application.Features.Queries.Providers.GetAllProviders;
using CQRS.Domain.Entities;
using CQRS.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CQRS.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;

        public HomeController(ILogger<HomeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            GetFilteredOrdersQueryRequest request = new() { StartDate = DateTime.Now.AddMonths(-1), EndDate = DateTime.Now };
            GetFilteredOrdersQueryResponse response = new();

            try
            {
                response = await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                //Errors are logged to console
                _logger.LogError(ex.Message);
            }

            return View(response.Orders);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            GetOrderQueryResponse response = new();

            try
            {
                response = await _mediator.Send(new GetOrderQueryRequest() { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            if (response == null)
            {
                return NotFound();
            }

            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrder()
        {
            AddOrderCommandRequest orderCommandRequest = new AddOrderCommandRequest();

            GetProvidersQueryResponse providersResponse = new();
            GetOrderItemsQueryResponse orderItemsResponse = new();

            try
            {
                providersResponse = await _mediator.Send(new GetProvidersQueryRequest());
                orderItemsResponse = await _mediator.Send(new GetOrderItemsQueryRequest());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Index");
            }

            providersResponse.Providers.ForEach(p => orderCommandRequest.Providers.Add(new() { Text = p.Name, Value = p.Id.ToString() }));
            orderItemsResponse.OrderItems.ForEach(o => orderCommandRequest.OrderItems.Add(new() { Text = o.Name, Value = o.Id.ToString() }));

            return View(orderCommandRequest);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(AddOrderCommandRequest orderCommandRequest)
        {
            AddOrderCommandResponse response = new();

            try
            {
                response = await _mediator.Send(orderCommandRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("AddOrder");
            }

            if (response.success == false)
            {
                TempData["error"] = response.message;
                return RedirectToAction("AddOrder");
            }

            TempData["success"] = "Order has been added successfully!";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditOrder(int id)
        {
            GetOrderQueryResponse orderResponse = new();
            GetProvidersQueryResponse providersResponse = new();
            GetOrderItemsQueryResponse orderItemsResponse = new();

            try
            {
                orderResponse = await _mediator.Send(new GetOrderQueryRequest() { Id = id });
                providersResponse = await _mediator.Send(new GetProvidersQueryRequest());
                orderItemsResponse = await _mediator.Send(new GetOrderItemsQueryRequest());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                RedirectToAction("Index");
            }

            EditOrderCommandRequest orderCommandRequest = new EditOrderCommandRequest() { Id = id, ProviderId = orderResponse.Order.ProviderId, OrderItemId = orderResponse.Order.OrderItemId, Number = orderResponse.Order.Number };

            providersResponse.Providers.ForEach(p =>
            {
                if (p.Id == orderCommandRequest.ProviderId)
                    orderCommandRequest.Providers.Add(new() { Text = p.Name, Value = p.Id.ToString(), Selected = true });
                else
                    orderCommandRequest.Providers.Add(new() { Text = p.Name, Value = p.Id.ToString() });
            });
            orderItemsResponse.OrderItems.ForEach(o =>
            {
                if (o.Id == orderCommandRequest.OrderItemId)
                    orderCommandRequest.OrderItems.Add(new() { Text = o.Name, Value = o.Id.ToString(), Selected = true });
                else
                    orderCommandRequest.OrderItems.Add(new() { Text = o.Name, Value = o.Id.ToString() });
            });

            return View(orderCommandRequest);
        }

        [HttpPost]
        public async Task<IActionResult> EditOrder(EditOrderCommandRequest orderRequest)
        {
            EditOrderCommandResponse response = new();

            try
            {
                response = await _mediator.Send(orderRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                RedirectToAction("Index");
            }

            if (response.success == false)
            {
                TempData["error"] = response.message;
                return RedirectToAction("Index");
            }

            TempData["success"] = "Order has been added successfully!";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            GetOrderQueryResponse order = new();

            try
            {
                order = await _mediator.Send(new GetOrderQueryRequest() { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                RedirectToAction("Index");
            }

            return View(order.Order);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrderPost(int id)
        {
            GetOrderQueryResponse order = new();
            DeleteOrderCommandResponse response = new();

            try
            {
                order = await _mediator.Send(new GetOrderQueryRequest() { Id = id });
                response = await _mediator.Send(new DeleteOrderCommandRequest() { Order = order.Order });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                RedirectToAction("Index");
            }

            if (response.success == false)
            {
                TempData["error"] = "Could not delete the order. Please try again";
                return RedirectToAction("Index");
            }

            TempData["success"] = "Order has been deleted successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> FilterByDate(DateTime startDate, DateTime endDate)
        {
            GetFilteredOrdersQueryRequest request = new() { StartDate = startDate, EndDate = endDate };
            GetFilteredOrdersQueryResponse response = new();

            try
            {
                response = await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                RedirectToAction("Index");
            }

            return PartialView("_OrderPartial", response.Orders);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}