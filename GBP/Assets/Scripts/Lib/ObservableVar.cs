using System;
using System.Collections.Generic;

[Serializable]
public class ObservableVar<T> : IObservableVar<T>
{
	public event Action<T, T> OnValueChanged;

	private T _value;

	public T Value
	{
		get => _value;
		set
		{
			T previousValue = _value;
			_value = value;
			if (OnValueChanged != null)
			{
				OnValueChanged.Invoke(previousValue, _value);
			}
		}
	}

	public ObservableVar()
	{
		_value = default(T);
	}

	public ObservableVar(T initValue)
	{
		_value = initValue;
	}

	public void Set(T newValue)
	{
		Value = newValue;
	}

	public void SetOnce(T neValue)
	{
		if (!EqualityComparer<T>.Default.Equals(_value, neValue))
		{
			Value = neValue;
		}
	}

	private int _lazyCounter;

	public void SetLazy(T newValue)
	{
		var prev = _value;
		_value = newValue;
		_lazyCounter++;
		if (_lazyCounter == 2)
		{
			OnValueChanged?.Invoke(prev, _value);
			_lazyCounter = 0;
		}
	}

	public override string ToString()
	{
		return _value.ToString();
	}

	public static implicit operator T(ObservableVar<T> observableVar) => observableVar.Value;
}