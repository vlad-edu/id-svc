using System;
using System.Runtime.Serialization;

namespace IdService.Core.Exceptions
{
    public sealed class InvalidConfigurationException : Exception
    {
        private readonly string? _keyName;

        public InvalidConfigurationException()
            : base("Invalid configuration.")
        {
        }

        public InvalidConfigurationException(string? message)
            : base(message)
        {
        }

        public InvalidConfigurationException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public InvalidConfigurationException(string? message, string? keyName, Exception? innerException)
            : base(message, innerException)
        {
            _keyName = keyName;
        }

        public InvalidConfigurationException(string? message, string? keyName)
            : base(message)
        {
            _keyName = keyName;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("KeyName", _keyName, typeof(string));
        }
    }
}