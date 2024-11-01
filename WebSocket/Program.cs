﻿using WebSocket.SocketServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Thêm dịch vụ cho SocketServers
builder.Services.AddHostedService<SocketServers>(); // Đăng ký SocketServers
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// C?u hình WebSocket

var webSocketOptions = new WebSocketOptions()
{
    KeepAliveInterval = TimeSpan.FromMinutes(10) // thời gian dữ kết nối
};
app.UseWebSockets(webSocketOptions);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
