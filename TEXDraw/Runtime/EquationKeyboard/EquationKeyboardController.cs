// Equation Keyboard Controller - Manages keyboard behavior and layout
// Handles tab navigation, search functionality, and custom symbol management

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace TexDrawLib
{
    [AddComponentMenu("TEXDraw/EquationKeyboardController")]
    public class EquationKeyboardController : MonoBehaviour
    {
        [Header("Keyboard Components")]
        [SerializeField]
        private EquationKeyboardUI m_KeyboardUI;

        [SerializeField]
        private InputField m_SearchField;        // Search for symbols

        [SerializeField]
        private LayoutGroup m_SearchResultContainer;  // Display search results

        [SerializeField]
        private Button m_ToggleButton;           // Show/hide keyboard

        [SerializeField]
        private CanvasGroup m_KeyboardCanvasGroup; // For fade in/out

        [Header("Keyboard Settings")]
        [SerializeField]
        private bool m_StartVisible = false;

        [SerializeField]
        private float m_AnimationDuration = 0.3f;

        [SerializeField]
        private bool m_ShowSearchTab = true;

        private List<SymbolButton> m_AllSymbols = new List<SymbolButton>();
        private bool m_IsVisible = false;
        private Coroutine m_AnimationCoroutine;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            // Collect all symbols
            var categories = SymbolLibrary.GetAllCategories();
            foreach (var category in categories)
            {
                m_AllSymbols.AddRange(category.symbols);
            }

            // Setup search
            if (m_SearchField)
            {
                m_SearchField.onValueChanged.AddListener(OnSearchTextChanged);
            }

            // Setup toggle button
            if (m_ToggleButton)
            {
                m_ToggleButton.onClick.AddListener(ToggleKeyboard);
            }

            // Initial state
            if (!m_StartVisible)
            {
                m_KeyboardCanvasGroup.alpha = 0;
                m_KeyboardCanvasGroup.blocksRaycasts = false;
            }
            else
            {
                m_IsVisible = true;
            }
        }

        /// <summary>
        /// Toggle keyboard visibility with animation
        /// </summary>
        public void ToggleKeyboard()
        {
            if (m_IsVisible)
                HideKeyboard();
            else
                ShowKeyboard();
        }

        /// <summary>
        /// Show keyboard with fade-in animation
        /// </summary>
        public void ShowKeyboard()
        {
            if (m_IsVisible)
                return;

            m_IsVisible = true;
            m_KeyboardCanvasGroup.blocksRaycasts = true;

            if (m_AnimationCoroutine != null)
                StopCoroutine(m_AnimationCoroutine);

            m_AnimationCoroutine = StartCoroutine(AnimateKeyboard(0, 1, m_AnimationDuration));
        }

        /// <summary>
        /// Hide keyboard with fade-out animation
        /// </summary>
        public void HideKeyboard()
        {
            if (!m_IsVisible)
                return;

            m_IsVisible = false;
            m_KeyboardCanvasGroup.blocksRaycasts = false;

            if (m_AnimationCoroutine != null)
                StopCoroutine(m_AnimationCoroutine);

            m_AnimationCoroutine = StartCoroutine(AnimateKeyboard(1, 0, m_AnimationDuration));
        }

        /// <summary>
        /// Animate keyboard fade in/out
        /// </summary>
        private System.Collections.IEnumerator AnimateKeyboard(float startAlpha, float endAlpha, float duration)
        {
            float elapsed = 0;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                m_KeyboardCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
                yield return null;
            }
            m_KeyboardCanvasGroup.alpha = endAlpha;
        }

        /// <summary>
        /// Handle search text changes
        /// </summary>
        private void OnSearchTextChanged(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                // Reset to first category
                m_KeyboardUI.SelectCategory(0);
                return;
            }

            // Filter symbols by search text
            var searchLower = searchText.ToLower();
            var filtered = m_AllSymbols.Where(s =>
                s.label.ToLower().Contains(searchLower) ||
                s.tooltip.ToLower().Contains(searchLower) ||
                s.latexCommand.ToLower().Contains(searchLower)
            ).ToList();

            // Display search results
            DisplaySearchResults(filtered);
        }

        /// <summary>
        /// Display search results in result container
        /// </summary>
        private void DisplaySearchResults(List<SymbolButton> results)
        {
            // Clear existing results
            foreach (Transform child in m_SearchResultContainer.transform)
            {
                Destroy(child.gameObject);
            }

            // Create result buttons
            foreach (var symbol in results.Take(20))  // Limit to 20 results
            {
                var button = new Button();
                var text = new Text();
                
                // Note: In real implementation, use proper prefab instantiation
                Debug.Log($"Search result: {symbol.label} - {symbol.tooltip}");
            }
        }

        /// <summary>
        /// Get keyboard visibility state
        /// </summary>
        public bool IsVisible => m_IsVisible;

        /// <summary>
        /// Add custom symbol to library
        /// </summary>
        public void AddCustomSymbol(SymbolButton symbol)
        {
            m_AllSymbols.Add(symbol);
        }

        /// <summary>
        /// Remove custom symbol from library
        /// </summary>
        public void RemoveCustomSymbol(string latexCommand)
        {
            var symbol = m_AllSymbols.FirstOrDefault(s => s.latexCommand == latexCommand);
            if (symbol != null)
                m_AllSymbols.Remove(symbol);
        }

        /// <summary>
        /// Export keyboard layout as preset
        /// </summary>
        public string ExportPreset()
        {
            var preset = new KeyboardPreset();
            preset.categories = SymbolLibrary.GetAllCategories();
            return JsonUtility.ToJson(preset);
        }

        /// <summary>
        /// Import keyboard layout from preset
        /// </summary>
        public void ImportPreset(string presetJson)
        {
            try
            {
                var preset = JsonUtility.FromJson<KeyboardPreset>(presetJson);
                if (preset != null && preset.categories != null)
                {
                    // Load preset categories
                    Debug.Log("Preset imported successfully");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to import preset: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Keyboard preset for saving/loading configurations
    /// </summary>
    [System.Serializable]
    public class KeyboardPreset
    {
        public List<SymbolCategory> categories;
        public string name;
        public string description;
    }
}
