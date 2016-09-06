using System;
using System.Xml.Linq;

// ReSharper disable CheckNamespace
namespace UPnP
// ReSharper restore CheckNamespace
{
    /// <summary>
    ///     Event handler for when a state variable changed.
    /// </summary>
    /// <param name="sender">The sender of the events.</param>
    /// <param name="eventArgs">The event arguments.</param>
    public delegate void StateVariableChangedEventHandler(object sender, StateVariableChangedEventArgs eventArgs);

    /// <summary>
    ///     Event handler for when a state variable changed.
    /// </summary>
    /// <typeparam name="T">The data type for the state variable.</typeparam>
    /// <param name="sender">The sender of the events.</param>
    /// <param name="eventArgs">The event arguments.</param>
    public delegate void StateVariableChangedEventHandler<in T>(object sender, T eventArgs);

    /// <summary>
    ///     Encapsulates the event arguments for when an evented state variable.
    /// </summary>
    public class StateVariableChangedEventArgs : EventArgs
    {
        #region Protected Locals

        /// <summary>
        ///     The state variable name which changed.
        /// </summary>
        protected string MsStateVarName;

        /// <summary>
        ///     The new state variable value.
        /// </summary>
        protected XElement MtStateVarValue;

        #endregion

        #region Public Methods

        /// <summary>
        ///     Creates a new state variable changed event arguments.
        /// </summary>
        /// <param name="stateVarName">The state variable name.</param>
        /// <param name="stateVarValue">The state variable value.</param>
        public StateVariableChangedEventArgs(string stateVarName, XElement stateVarValue)
        {
            MsStateVarName = stateVarName;
            MtStateVarValue = stateVarValue;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the state variable name which changed.
        /// </summary>
        public string StateVarName
        {
            get { return MsStateVarName; }
        }

        /// <summary>
        ///     Gets the new state variable value.
        /// </summary>
        public XElement StateVarValue
        {
            get { return MtStateVarValue; }
        }

        #endregion
    }
}