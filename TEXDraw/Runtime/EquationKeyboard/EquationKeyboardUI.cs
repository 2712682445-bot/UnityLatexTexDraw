// Equation Keyboard UI - Displays symbol categories as tabs and symbols as buttons
// Allows users to click symbols to insert them into the active text input

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TexDrawLib
{
    [AddComponentMenu("TEXDraw/EquationKeyboardUI"), SelectionBase]
    public class EquationKeyboardUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private TEXInput m_TargetInput;           // Input to receive symbols

        [SerializeField]
        private LayoutGroup m_TabContainer;      // Container for category tabs

        [SerializeField]
        private LayoutGroup m_ButtonContainer;   // Container for symbol buttons

        [Header("Prefabs")]
        [SerializeField]
        private Button m_TabButtonPrefab;        // Prefab for category tabs

        [SerializeField]
        private Button m_SymbolButtonPrefab;     // Prefab for symbol buttons

        [Header("Display Settings")]
        [SerializeField]
        private Color m_ActiveTabColor = new Color(0.2f, 0.4f, 0.8f, 1f);  // Active tab color

        [SerializeField]
        private Color m_InactiveTabColor = new Color(0.8f, 0.8f, 0.8f, 1f); // Inactive tab color

        [SerializeField]
        private int m_ButtonsPerRow = 6;         // How many buttons per row

        private List<Button> m_TabButtons = new List<Button>();
        private List<SymbolCategory> m_Categories = new List<SymbolCategory>();
        private int m_ActiveCategoryIndex = 0;

        private void Start()
        {
            Initialize();
        }

        /// <summary>
        /// Initialize the keyboard with all symbol categories
        /// </summary>
        private void Initialize()
        {
            m_Categories = SymbolLibrary.GetAllCategories();
            CreateTabs();
            SelectCategory(0);
        }

        /// <summary>
        /// Create tab buttons for each category
        /// </summary>
        private void CreateTabs()
        {
            // Clear existing tabs
            foreach (Transform child in m_TabContainer.transform)
            {
                Destroy(child.gameObject);
            }
            m_TabButtons.Clear();

            // Create new tabs
            for (int i = 0; i < m_Categories.Count; i++)
            {
                var category = m_Categories[i];
                var tabButton = Instantiate(m_TabButtonPrefab, m_TabContainer.transform, false);
                var buttonText = tabButton.GetComponentInChildren<Text>();
                
                if (buttonText)
                    buttonText.text = category.icon;  // Show icon
                
                var tooltip = tabButton.gameObject.AddComponent<Tooltip>();
                tooltip.text = category.name;
                
                int categoryIndex = i;  // Capture for closure
                tabButton.onClick.AddListener(() => SelectCategory(categoryIndex));
                
                m_TabButtons.Add(tabButton);
            }
        }

        /// <summary>
        /// Select a category and populate symbol buttons
        /// </summary>
        public void SelectCategory(int index)
        {
            if (index < 0 || index >= m_Categories.Count)
                return;

            m_ActiveCategoryIndex = index;

            // Update tab colors
            for (int i = 0; i < m_TabButtons.Count; i++)
            {
                var buttonImage = m_TabButtons[i].GetComponent<Image>();
                buttonImage.color = (i == index) ? m_ActiveTabColor : m_InactiveTabColor;
            }

            // Populate symbol buttons
            PopulateSymbolButtons(m_Categories[index]);
        }

        /// <summary>
        /// Create symbol buttons for the selected category
        /// </summary>
        private void PopulateSymbolButtons(SymbolCategory category)
        {
            // Clear existing buttons
            foreach (Transform child in m_ButtonContainer.transform)
            {
                Destroy(child.gameObject);
            }

            // Create grid layout if needed
            var gridLayout = m_ButtonContainer as GridLayoutGroup;
            if (gridLayout)
            {
                gridLayout.constraintCount = m_ButtonsPerRow;
            }

            // Create new symbol buttons
            for (int i = 0; i < category.symbols.Count; i++)
            {
                var symbol = category.symbols[i];
                var button = Instantiate(m_SymbolButtonPrefab, m_ButtonContainer.transform, false);
                
                var buttonText = button.GetComponentInChildren<Text>();
                if (buttonText)
                    buttonText.text = symbol.label;

                var tooltip = button.gameObject.AddComponent<Tooltip>();
                tooltip.text = symbol.tooltip;

                // Set button click action
                var symbolData = symbol;  // Capture for closure
                button.onClick.AddListener(() => InsertSymbol(symbolData));
            }
        }

        /// <summary>
        /// Insert symbol into the target input
        /// </summary>
        private void InsertSymbol(SymbolButton symbol)
        {
            if (m_TargetInput == null)
                return;

            var command = symbol.latexCommand;
            
            // Add braces if needed
            if (symbol.insertBraces && !command.StartsWith("{") && !command.StartsWith("("))
            {
                command = "{" + command + "}";
            }

            // Insert at cursor position
            var text = m_TargetInput.text;
            int cursorPos = m_TargetInput.selectionStart + m_TargetInput.selectionLength;
            
            text = text.Insert(cursorPos, command);
            m_TargetInput.text = text;
            
            // Move cursor after inserted text
            m_TargetInput.selectionStart = cursorPos + command.Length;
            m_TargetInput.selectionLength = 0;
        }

        /// <summary>
        /// Quick access methods for inserting common symbols
        /// </summary>
        public void InsertFraction()
        {
            InsertSymbol(new SymbolButton("fraction", "\\frac{numerator}{denominator}", "Fraction", false));
        }

        public void InsertSquareRoot()
        {
            InsertSymbol(new SymbolButton("√", "\\sqrt{}", "Square root", false));
        }

        public void InsertSuperscript()
        {
            InsertSymbol(new SymbolButton("x²", "^{}", "Superscript", false));
        }

        public void InsertSubscript()
        {
            InsertSymbol(new SymbolButton("x₂", "_{}", "Subscript", false));
        }

        public void InsertIntegral()
        {
            InsertSymbol(new SymbolButton("∫", "\\int", "Integral", false));
        }

        public void InsertMatrix()
        {
            var matrixCommand = "\\begin{matrix} & \\\\ & \\end{matrix}";
            InsertSymbol(new SymbolButton("matrix", matrixCommand, "Matrix", false));
        }
    }

    /// <summary>
    /// Simple tooltip component
    /// </summary>
    public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string text;
        private Text m_TooltipText;
        private Canvas m_TooltipCanvas;

        public void OnPointerEnter(PointerEventData eventData)
        {
            // Show tooltip (implementation depends on your UI setup)
            Debug.Log(text);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // Hide tooltip
        }
    }
}
