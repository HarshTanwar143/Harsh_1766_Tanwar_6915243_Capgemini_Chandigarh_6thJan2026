using System.Reflection;
using log4net;
using log4net.Config;

var builder = WebApplication.CreateBuilder(args);

// Setup Log4net
var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly()!);
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
