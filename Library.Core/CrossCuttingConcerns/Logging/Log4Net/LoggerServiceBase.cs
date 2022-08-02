using log4net;
using log4net.Core;
using log4net.Repository;
using System.Reflection;
using System.Xml;

namespace Core.CrossCuttingConcerns.Logging.Log4Net
{
    public class LoggerServiceBase
    {
        private readonly ILog _log;
        public LoggerServiceBase(string name)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(File.OpenRead("log4net.config"));

            ILoggerRepository loggerRepository = LoggerManager
                .CreateRepository(Assembly.GetEntryAssembly(), 
                typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(loggerRepository, xmlDocument["log4net"]);
            _log = LogManager.GetLogger(loggerRepository.Name,name);
        }

        public bool IsInfoEnabled => this._log.IsInfoEnabled;
        public bool IsDebugEnabled => this._log.IsDebugEnabled;
        public bool IsWarnEnabled => this._log.IsWarnEnabled;
        public bool IsFatalEnabled => this._log.IsFatalEnabled;
        public bool IsErrorEnabled => this._log.IsErrorEnabled;

        public void Info(object logMessage)
        {
            if(IsInfoEnabled) _log.Info(logMessage);
        }

        public void Warn(object logMessage)
        {
            if(IsWarnEnabled) _log.Warn(logMessage);
        }

        public void Debug(object logMessage)
        {
            if(IsDebugEnabled) _log.Debug(logMessage);
        }
        public void Fatal(object logMessage)
        {
            if(IsFatalEnabled) _log.Fatal(logMessage);
        }

        public void Error(object logMessage)
        {
            if(IsErrorEnabled) _log.Error(logMessage);
        }
    }
}
