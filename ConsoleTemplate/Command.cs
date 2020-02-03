// (c) 2020 Moonwave Interactive. All rights reserved under MIT License.

using System;

namespace ConsoleTemplate
{
    /// <summary>
    /// Class that represents a command object. This class cannot be inherited
    /// </summary>
    public sealed class Command
    {
        private readonly Action<string[]> action;

        /// <summary>
        /// Gets the <see cref="CommandInfo"/> for this command.
        /// </summary>
        public CommandInfo CommandInfo { get; }

        /// <summary>
        /// Gets a value which indicates whether arguments after
        /// the command should be tokenized or processed verbatim.
        /// </summary>
        public bool PreserveInput { get; }

        /// <summary>
        /// Creates and initializes a new instance of a <see cref="Command"/>
        /// object.
        /// </summary>
        /// <param name="action">
        /// The name of the command.
        /// </param>
        /// <param name="commandInfo">
        /// The <see cref="CommandInfo"/> for the command.
        /// </param>
        /// <param name="preserveInput">
        /// Sets whether input after the command should be tokenized or
        /// processed verbatim.
        /// </param>
        public Command(Action<string[]> action, CommandInfo commandInfo, bool preserveInput)
        {
            this.action = action ?? throw new ArgumentNullException("Action cannot be null.");
            CommandInfo = commandInfo ?? throw new ArgumentNullException("CommandInfo cannot be null.");
            PreserveInput = preserveInput;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="args">
        /// Arguments to be supplied to the command.
        /// </param>
        public void Execute(string[] args)
        {
            action.Invoke(args);
        }
    }
}