using TMPro;
using UnityEngine;

namespace UI
{
    public class NumericInputField : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;

        private void Start()
        {
            _inputField.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(string input)
        {
            if (!IsNumeric(input))
            {
                _inputField.text = input.Substring(0, input.Length - 1);
            }
        }

        private bool IsNumeric(string text)
        {
            foreach (var c in text)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
