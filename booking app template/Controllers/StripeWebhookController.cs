﻿using Microsoft.AspNetCore.Mvc;
using booking_app_template.Data.Cart;
using booking_app_template.Data.Services;
using booking_app_template.Models;
using Stripe;
using Stripe.Checkout;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using booking_app_template.Data;

namespace booking_app_template.Controllers
{
    [Route("webhook")]
    [ApiController]
    public class WebhookController : Controller
    {

        private readonly IProductService _productService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderService _orderService;
        private readonly StripeSettings _stripeSettings;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;

        public WebhookController(AppDbContext context, IConfiguration configuration, IProductService productsService, ShoppingCart shoppingCart, IOrderService ordersService, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _productService = productsService;
            _shoppingCart = shoppingCart;
            _orderService = ordersService;
            _userManager = userManager;
            _context = context;
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            //For retrieving Appsettings.
            var configuration = new ConfigurationBuilder()
           .SetBasePath(Environment.CurrentDirectory)
           .AddJsonFile("appsettings.json")
           .Build();

            // Retrieve Azure Key Vault settings from appsettings.json
            string endpointSecret = _configuration["StripeSettings:EndpointSecret"];
            
            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                var signatureHeader = Request.Headers["Stripe-Signature"];

                stripeEvent = EventUtility.ConstructEvent(json,
                        signatureHeader, endpointSecret);

                if (stripeEvent.Type == Events.CheckoutSessionCompleted || stripeEvent.Type == Events.CheckoutSessionAsyncPaymentSucceeded)
                {
                    var session = stripeEvent.Data.Object as Session;

                    var order = _context.Orders
                       .Include(o => o.OrderItems)
                       .FirstOrDefault(o => o.CheckoutId == session.Id);

                    if (order.CheckoutId == session.Id)
                    {
                        order.OrderPaid = session.PaymentStatus;
                        _context.SaveChanges();
                    }
                    else
                    {
                        order.OrderPaid = "Error with Payment, please contact for support quoting order Id.";
                        _context.SaveChanges();
                    }
                }

                else if (stripeEvent.Type == Events.PaymentMethodAttached)
                {
                    var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                    // Then define and call a method to handle the successful attachment of a PaymentMethod.
                    // handlePaymentMethodAttached(paymentMethod);
                }
                
                else
                {
                    Console.Write("Unhandled event type: {0}", stripeEvent.Type);
                }
                
                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        
    }
}


