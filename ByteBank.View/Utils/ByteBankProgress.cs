using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.View.Utils
{
    public class ByteBankProgress<T> : IProgress<T>
    {
        private readonly Action<T> _action;
        private readonly TaskScheduler _taskScheduler; 

        public ByteBankProgress(Action<T> action)
        {
            _taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();/*Obtendo contexto(thread) responsavel por manipular a UI*/
            _action = action; /*delegate que executa a tarefa de incrementação do progress*/
        }

        public void Report(T value)
        {   /*
            Uma tarefa (ou task) representa uma unidade de trabalho que deverá ser realizada. 
            Essa unidade de trabalho pode rodar em uma thread separada.
            https://imasters.com.br/framework/dotnet/c-apresentando-tasks/?trace=1519021197            
             */
            Task.Factory.StartNew(
                    () => _action(value),
                    System.Threading.CancellationToken.None,//parametro obrigatorio
                    TaskCreationOptions.None,//parametro obrigatorio
                    _taskScheduler
                );
        }
    }
}
