﻿using BackgroundTask.IntegrationEvents.Events;
using EvenBus.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyBackgroundTask;
using MyBackgroundTask.Infrastructure;
using MyBackgroundTask.Model;
using MyBackgroundTask.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication1.Tasks
{
    public class TestBackgroundTask 
        : BackgroundService
    {
        private readonly ILogger<TestBackgroundTask> _logger;
        private readonly IEventBus _eventBus;
        private Task _executingTask;
        private readonly IProductRespository _context;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        //public TestBackgroundTask(ILogger<TestBackgroundTask> logger, IEventBus eventBus,IProductRespository context)
        public TestBackgroundTask(ILogger<TestBackgroundTask> logger, IEventBus eventBus, IProductRespository context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _context = context;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"Test Background Task Start");
            stoppingToken.Register(() =>
            _logger.LogDebug($"Test Background Task Stop")
            );

            //var sss = _context.Add(new Product());

            HttpService httpService = new HttpService();

            var dic = new Dictionary<string,string>();
            dic.Add("dataNum", "10");
            dic.Add("lotteryCode","1407");
            var resp = await httpService.PostForm("http://3dfe47bd7f4c8d70.dafacloudapp.com/v1/lottery/openResult?lotteryCode=1407&dataNum=10&", dic);

            var eventMessage = new UserErrorMessageIntegrationEvent("AKPay", "103", "h5", "簽名錯誤", DateTime.Now);

            _eventBus.Publish(eventMessage);
            
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("Test Task Doing Background Work");

                await Task.Delay(100, stoppingToken);
            }

            throw new NotImplementedException();
        }

        public virtual Task StartAsync(CancellationToken stoppingToken)
        {
            _executingTask = ExecuteAsync(_stoppingCts.Token);

            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }

            return Task.CompletedTask;
        }

        public virtual async Task StopAsync(CancellationToken stoppingToken)
        {
            if (_executingTask == null)
            {
                return;
            }

            try
            {
                _stoppingCts.Cancel();
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Stop Error:{ex.Message} ");
            }
            finally
            {
                await Task.WhenAny(_executingTask, Task.Delay(100));
            }

        }

        public virtual void Dispose()
        {
            _stoppingCts.Cancel();
        }
    }
}
