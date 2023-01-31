using System;

public interface IObservableVar<out T>
{
	event Action<T, T> OnValueChanged;
	T Value { get; }
}