using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bb.WebHost.Services
{


    public abstract class HostedBackgroupdService : BackgroundService
    {

        public HostedBackgroupdService()
        {
            KilledGracefullInterceptor.Append(this);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public bool Stoping { get => KilledGracefullInterceptor.ApplicationStoping; }

    }

}
