// (c) 2020 Moonwave Interactive. All rights reserved under MIT License.

namespace ConsoleTemplate
{
    /// <summary>
    /// Class that represents a command information object. This class cannot be inherited.
    /// </summary>
    public sealed class CommandInfo
    {
        private string _name;

        /// <summary>
        /// 
        /// </summary>
        public string Name => _name;

        private string _description;

        /// <summary>
        /// 
        /// </summary>
        public string Description => _description;

        private string _extendedInfo;

        /// <summary>
        /// 
        /// </summary>
        public string ExtendedInfo => _extendedInfo;

        private Parameter[] _parameters;

        /// <summary>
        /// 
        /// </summary>
        public Parameter[] Parameters => _parameters;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public CommandInfo(string name)
        {
            Init(name, null, null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public CommandInfo(string name, string description)
        {
            Init(name, description, null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="extendedInfo"></param>
        public CommandInfo(string name, string description, string extendedInfo)
        {
            Init(name, description, extendedInfo, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="helpText"></param>
        /// <param name="parameters"></param>
        public CommandInfo(string name, string description, string extendedInfo, params Parameter[] parameters)
        {
            Init(name, description, extendedInfo, parameters);
        }

        private void Init(string name, string description, string extendedInfo, params Parameter[] parameters)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new System.ArgumentNullException("Command name cannot be null or emoty.");

            _name = name;

            if (string.IsNullOrWhiteSpace(description))
                _description = "No descripton is available for this command.";
            else
                _description = description;

            _extendedInfo = extendedInfo;
            _parameters = parameters;
        }
    }
}