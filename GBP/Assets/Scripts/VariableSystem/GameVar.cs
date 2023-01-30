using System;

public class GameVar
{
	public event Action<GameVar, string> OnValueChanged;
	public string Name { get; private set; }
	private string _storedValue;

	public string Value
	{
		get => _storedValue;
		set
		{
			if (_storedValue != value)
			{
				_storedValue = value;
				if (OnValueChanged != null)
				{
					OnValueChanged.Invoke(this, value);
				}
			}
		}
	}

	public GameVar(string name, string initialValue)
	{
		Name = name;
		_storedValue = initialValue;
	}
}
