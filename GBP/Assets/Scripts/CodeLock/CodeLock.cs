using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CodeLock : Puzzle
{
	[SerializeField] UnityEvent OnSuccess;
	[SerializeField] UnityEvent OnFail;

	[SerializeField] private TextMesh textCode;
	[SerializeField] private Button backButton;
	[SerializeField] private Camera raycastCamera;

	[SerializeField] private Color errorColor = Color.red;
	[SerializeField] private string errorString = "FAIL";
	
	[SerializeField] private Color validColor = Color.green;
	[SerializeField] private string validString = "OK";
	
	[SerializeField] private Color defaultColor = Color.yellow;
	
	[SerializeField] private string storedPassword;
	private int _layerMask;

	private string _textCodeValue = "";
	private bool _blockInput; 

	private void Awake()
	{
		_layerMask = 1 << LayerMask.NameToLayer("CodeLock");
		backButton.onClick.AddListener(OnBackButtonClick);
		textCode.color = defaultColor;
	}

	private void Update()
	{
		UpdateRaycast();
	}

	private void UpdateRaycast()
	{
		if (_blockInput)
			return;
		
		if (Input.GetMouseButtonDown(0))
		{
			Ray mRay = raycastCamera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(mRay.origin, mRay.direction, out RaycastHit hitInfo, 100, _layerMask))
			{
				if (hitInfo.collider.gameObject.TryGetComponent(out CodeLockButton button))
				{
					button.Press();
				}
			}
		}
	}

	public void SetPassword(string password)
	{
		storedPassword = password;
	}

	public void AddDigit(string digit)
	{
		if (_textCodeValue.Length < storedPassword.Length)
		{
			_textCodeValue += digit;
			SetScreen(_textCodeValue);
		}
	}

	public void DeleteDigit()
	{
		if (_textCodeValue.Length > 0)
		{
			_textCodeValue = _textCodeValue.Remove(_textCodeValue.Length - 1);
			SetScreen(_textCodeValue);
		}
	}

	public void Enter()
	{
		if (_textCodeValue == storedPassword)
		{
			StartCoroutine(Success());
		}
		else
		{
			StartCoroutine(Fail());
		}
	}

	private void SetScreen(string value)
	{
		textCode.text = value;
	}

	private IEnumerator Success()
	{
		_blockInput = true;
		textCode.color = validColor;
		SetScreen(validString);
		OnSuccess.Invoke();
		yield return new WaitForSecondsRealtime(0.5f);
		state.Set(EState.Complete);
		_textCodeValue = "";
		textCode.color = defaultColor;
		_blockInput = false;
		Hide();
	}

	private IEnumerator Fail()
	{
		_blockInput = true;
		OnFail.Invoke();
		textCode.color = errorColor;
		
		_textCodeValue = errorString;
		SetScreen(_textCodeValue);
		yield return new WaitForSecondsRealtime(0.3f);
		
		_textCodeValue = "";
		SetScreen(_textCodeValue);
		yield return new WaitForSecondsRealtime(0.3f);
		
		_textCodeValue = errorString;
		SetScreen(_textCodeValue);
		yield return new WaitForSecondsRealtime(0.3f);
		state.Set(EState.Failed);
		_textCodeValue = "";
		textCode.color = defaultColor;
		SetScreen(_textCodeValue);
		_blockInput = false;
	}

	private void OnBackButtonClick()
	{
		_blockInput = false;
		_textCodeValue = "";
		textCode.color = defaultColor;
		Hide();
	}
}