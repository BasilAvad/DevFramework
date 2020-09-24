using log4net.Core;
using log4net.Layout;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Layouts
{
    public class JonLayout : LayoutSkeleton
    {
        public override void ActivateOptions()
        {
           
        }

        public override void Format(TextWriter writer, LoggingEvent loggingEvent)
        {
            var LogEvent = new SerializableLogEvent(loggingEvent);
            var Json = JsonConvert.SerializeObject(LogEvent,Formatting.Indented);
            writer.WriteLine(Json);

        }
    }
}
