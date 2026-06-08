// Symbol Category Definitions - Organizes mathematical/chemical symbols by category
// Enables users to switch between tabs and find specific symbols quickly

using System;
using System.Collections.Generic;
using UnityEngine;

namespace TexDrawLib
{
    /// <summary>
    /// Defines different symbol categories available in the equation keyboard
    /// </summary>
    [System.Serializable]
    public class SymbolCategory
    {
        [SerializeField]
        public string name;                    // Category name (e.g., "Greek", "Operators")

        [SerializeField]
        public string icon;                    // Unicode icon or emoji for tab display

        [SerializeField]
        public List<SymbolButton> symbols;    // Symbols in this category

        public SymbolCategory()
        {
            symbols = new List<SymbolButton>();
        }

        public SymbolCategory(string name, string icon)
        {
            this.name = name;
            this.icon = icon;
            this.symbols = new List<SymbolButton>();
        }
    }

    /// <summary>
    /// Represents a single symbol button with LaTeX command and display properties
    /// </summary>
    [System.Serializable]
    public class SymbolButton
    {
        [SerializeField]
        public string label;                   // Display text on button (e.g., "α")

        [SerializeField]
        public string latexCommand;            // LaTeX command (e.g., "\\alpha")

        [SerializeField]
        public string tooltip;                 // Hover tooltip (e.g., "Greek letter alpha")

        [SerializeField]
        public bool insertBraces;              // Whether to auto-insert braces around command

        public SymbolButton() { }

        public SymbolButton(string label, string latexCommand, string tooltip, bool insertBraces = true)
        {
            this.label = label;
            this.latexCommand = latexCommand;
            this.tooltip = tooltip;
            this.insertBraces = insertBraces;
        }
    }

    /// <summary>
    /// Predefined symbol categories and their symbols
    /// </summary>
    public static class SymbolLibrary
    {
        // Greek Letters
        public static SymbolCategory GetGreekLetters()
        {
            var category = new SymbolCategory("Greek Letters", "Α");
            category.symbols.AddRange(new SymbolButton[]
            {
                new SymbolButton("α", "\\alpha", "Greek alpha"),
                new SymbolButton("β", "\\beta", "Greek beta"),
                new SymbolButton("γ", "\\gamma", "Greek gamma"),
                new SymbolButton("δ", "\\delta", "Greek delta"),
                new SymbolButton("ε", "\\epsilon", "Greek epsilon"),
                new SymbolButton("ζ", "\\zeta", "Greek zeta"),
                new SymbolButton("η", "\\eta", "Greek eta"),
                new SymbolButton("θ", "\\theta", "Greek theta"),
                new SymbolButton("ι", "\\iota", "Greek iota"),
                new SymbolButton("κ", "\\kappa", "Greek kappa"),
                new SymbolButton("λ", "\\lambda", "Greek lambda"),
                new SymbolButton("μ", "\\mu", "Greek mu"),
                new SymbolButton("ν", "\\nu", "Greek nu"),
                new SymbolButton("ξ", "\\xi", "Greek xi"),
                new SymbolButton("ο", "o", "Greek omicron"),
                new SymbolButton("π", "\\pi", "Greek pi"),
                new SymbolButton("ρ", "\\rho", "Greek rho"),
                new SymbolButton("σ", "\\sigma", "Greek sigma"),
                new SymbolButton("τ", "\\tau", "Greek tau"),
                new SymbolButton("υ", "\\upsilon", "Greek upsilon"),
                new SymbolButton("φ", "\\phi", "Greek phi"),
                new SymbolButton("χ", "\\chi", "Greek chi"),
                new SymbolButton("ψ", "\\psi", "Greek psi"),
                new SymbolButton("ω", "\\omega", "Greek omega"),
            });
            return category;
        }

        // Operators
        public static SymbolCategory GetOperators()
        {
            var category = new SymbolCategory("Operators", "±");
            category.symbols.AddRange(new SymbolButton[]
            {
                new SymbolButton("+", "+", "Plus", false),
                new SymbolButton("−", "-", "Minus", false),
                new SymbolButton("×", "\\times", "Multiplication"),
                new SymbolButton("÷", "\\div", "Division"),
                new SymbolButton("±", "\\pm", "Plus or minus"),
                new SymbolButton("∓", "\\mp", "Minus or plus"),
                new SymbolButton("·", "\\cdot", "Dot product"),
                new SymbolButton("*", "*", "Asterisk", false),
                new SymbolButton("/", "/", "Division", false),
                new SymbolButton("^", "^", "Power", false),
                new SymbolButton("√", "\\sqrt", "Square root"),
                new SymbolButton("∛", "\\sqrt[3]", "Cube root"),
                new SymbolButton("∜", "\\sqrt[4]", "Fourth root"),
                new SymbolButton("|", "|", "Absolute value", false),
                new SymbolButton("∣", "\\mid", "Divider"),
            });
            return category;
        }

        // Relations
        public static SymbolCategory GetRelations()
        {
            var category = new SymbolCategory("Relations", "=");
            category.symbols.AddRange(new SymbolButton[]
            {
                new SymbolButton("=", "=", "Equals", false),
                new SymbolButton("≠", "\\neq", "Not equal"),
                new SymbolButton("<", "<", "Less than", false),
                new SymbolButton(">", ">", "Greater than", false),
                new SymbolButton("≤", "\\leq", "Less than or equal"),
                new SymbolButton("≥", "\\geq", "Greater than or equal"),
                new SymbolButton("≪", "\\ll", "Much less than"),
                new SymbolButton("≫", "\\gg", "Much greater than"),
                new SymbolButton("≈", "\\approx", "Approximately equal"),
                new SymbolButton("≡", "\\equiv", "Equivalent"),
                new SymbolButton("~", "\\sim", "Similar"),
                new SymbolButton("∝", "\\propto", "Proportional to"),
                new SymbolButton("∈", "\\in", "Element of"),
                new SymbolButton("∉", "\\notin", "Not element of"),
                new SymbolButton("⊂", "\\subset", "Subset of"),
            });
            return category;
        }

        // Logic & Set Theory
        public static SymbolCategory GetLogicSets()
        {
            var category = new SymbolCategory("Logic & Sets", "∀");
            category.symbols.AddRange(new SymbolButton[]
            {
                new SymbolButton("∀", "\\forall", "For all"),
                new SymbolButton("∃", "\\exists", "There exists"),
                new SymbolButton("∄", "\\nexists", "Does not exist"),
                new SymbolButton("∧", "\\wedge", "And/Intersection"),
                new SymbolButton("∨", "\\vee", "Or/Union"),
                new SymbolButton("¬", "\\neg", "Not"),
                new SymbolButton("⊕", "\\oplus", "XOR"),
                new SymbolButton("⟹", "\\Rightarrow", "Implies"),
                new SymbolButton("⟸", "\\Leftarrow", "Implied by"),
                new SymbolButton("⟺", "\\Leftrightarrow", "If and only if"),
                new SymbolButton("∅", "\\emptyset", "Empty set"),
                new SymbolButton("∪", "\\cup", "Union"),
                new SymbolButton("∩", "\\cap", "Intersection"),
                new SymbolButton("⊆", "\\subseteq", "Subset or equal"),
            });
            return category;
        }

        // Calculus
        public static SymbolCategory GetCalculus()
        {
            var category = new SymbolCategory("Calculus", "∫");
            category.symbols.AddRange(new SymbolButton[]
            {
                new SymbolButton("∫", "\\int", "Integral"),
                new SymbolButton("∬", "\\iint", "Double integral"),
                new SymbolButton("∭", "\\iiint", "Triple integral"),
                new SymbolButton("∮", "\\oint", "Contour integral"),
                new SymbolButton("∂", "\\partial", "Partial derivative"),
                new SymbolButton("∇", "\\nabla", "Nabla/Gradient"),
                new SymbolButton("′", "'", "Prime (derivative)", false),
                new SymbolButton("″", "''", "Double prime", false),
                new SymbolButton("∞", "\\infty", "Infinity"),
                new SymbolButton("Δ", "\\Delta", "Capital Delta"),
                new SymbolButton("δ", "\\delta", "Delta"),
                new SymbolButton("lim", "\\lim", "Limit", false),
                new SymbolButton("→", "\\rightarrow", "Right arrow"),
                new SymbolButton("⟶", "\\longrightarrow", "Long right arrow"),
            });
            return category;
        }

        // Functions
        public static SymbolCategory GetFunctions()
        {
            var category = new SymbolCategory("Functions", "ƒ");
            category.symbols.AddRange(new SymbolButton[]
            {
                new SymbolButton("sin", "\\sin", "Sine", false),
                new SymbolButton("cos", "\\cos", "Cosine", false),
                new SymbolButton("tan", "\\tan", "Tangent", false),
                new SymbolButton("cot", "\\cot", "Cotangent", false),
                new SymbolButton("sec", "\\sec", "Secant", false),
                new SymbolButton("csc", "\\csc", "Cosecant", false),
                new SymbolButton("arcsin", "\\arcsin", "Arcsine", false),
                new SymbolButton("arccos", "\\arccos", "Arccosine", false),
                new SymbolButton("arctan", "\\arctan", "Arctangent", false),
                new SymbolButton("log", "\\log", "Logarithm", false),
                new SymbolButton("ln", "\\ln", "Natural logarithm", false),
                new SymbolButton("exp", "\\exp", "Exponential", false),
                new SymbolButton("min", "\\min", "Minimum", false),
                new SymbolButton("max", "\\max", "Maximum", false),
            });
            return category;
        }

        // Linear Algebra
        public static SymbolCategory GetLinearAlgebra()
        {
            var category = new SymbolCategory("Linear Algebra", "⟨⟩");
            category.symbols.AddRange(new SymbolButton[]
            {
                new SymbolButton("⟨⟩", "\\langle \\rangle", "Angle brackets"),
                new SymbolButton("|", "|", "Absolute value", false),
                new SymbolButton("‖", "\\|", "Norm/Length"),
                new SymbolButton("·", "\\cdot", "Dot product"),
                new SymbolButton("×", "\\times", "Cross product"),
                new SymbolButton("∗", "\\ast", "Convolution"),
                new SymbolButton("⊗", "\\otimes", "Tensor product"),
                new SymbolButton("⊥", "\\perp", "Perpendicular"),
                new SymbolButton("‖", "\\|", "Parallel"),
                new SymbolButton("†", "^\\dagger", "Conjugate transpose"),
                new SymbolButton("T", "^T", "Transpose"),
                new SymbolButton("⁻¹", "^{-1}", "Inverse"),
                new SymbolButton("det", "\\det", "Determinant", false),
                new SymbolButton("tr", "\\mathrm{tr}", "Trace", false),
            });
            return category;
        }

        // Chemistry
        public static SymbolCategory GetChemistry()
        {
            var category = new SymbolCategory("Chemistry", "H₂O");
            category.symbols.AddRange(new SymbolButton[]
            {
                new SymbolButton("→", "→", "Reaction forward", false),
                new SymbolButton("←", "←", "Reaction backward", false),
                new SymbolButton("⇌", "⇌", "Equilibrium", false),
                new SymbolButton("↑", "↑", "Gas evolved", false),
                new SymbolButton("↓", "↓", "Precipitate", false),
                new SymbolButton("=", "=", "Double bond", false),
                new SymbolButton("#", "#", "Triple bond", false),
                new SymbolButton("Δ", "Δ", "Heat", false),
                new SymbolButton("°", "°", "Degree", false),
                new SymbolButton("•", "•", "Radical dot", false),
                new SymbolButton("+", "+", "Charge positive", false),
                new SymbolButton("−", "−", "Charge negative", false),
                new SymbolButton("H₂O", "H_2O", "Water", false),
                new SymbolButton("O₂", "O_2", "Oxygen molecule", false),
            });
            return category;
        }

        // Brackets & Delimiters
        public static SymbolCategory GetBrackets()
        {
            var category = new SymbolCategory("Brackets", "()");
            category.symbols.AddRange(new SymbolButton[]
            {
                new SymbolButton("(", "(", "Left parenthesis", false),
                new SymbolButton(")", ")", "Right parenthesis", false),
                new SymbolButton("[", "[", "Left square bracket", false),
                new SymbolButton("]", "]", "Right square bracket", false),
                new SymbolButton("{", "\\{", "Left brace"),
                new SymbolButton("}", "\\}", "Right brace"),
                new SymbolButton("⌊", "\\lfloor", "Left floor"),
                new SymbolButton("⌋", "\\rfloor", "Right floor"),
                new SymbolButton("⌈", "\\lceil", "Left ceiling"),
                new SymbolButton("⌉", "\\rceil", "Right ceiling"),
                new SymbolButton("|", "|", "Pipe/Absolute value", false),
                new SymbolButton("‖", "\\|", "Double pipe/Norm"),
                new SymbolButton("⟨", "\\langle", "Left angle bracket"),
                new SymbolButton("⟩", "\\rangle", "Right angle bracket"),
            });
            return category;
        }

        // Other Symbols
        public static SymbolCategory GetOtherSymbols()
        {
            var category = new SymbolCategory("Other", "★");
            category.symbols.AddRange(new SymbolButton[]
            {
                new SymbolButton("°", "^\\circ", "Degree"),
                new SymbolButton("′", "'", "Prime", false),
                new SymbolButton("″", "''", "Double prime", false),
                new SymbolButton("℃", "^\\circ C", "Celsius"),
                new SymbolButton("™", "^{\\mathrm{TM}}", "Trademark"),
                new SymbolButton("©", "^\\copyright", "Copyright"),
                new SymbolButton("€", "\\euro", "Euro"),
                new SymbolButton("£", "\\pounds", "Pound"),
                new SymbolButton("¢", "\\cents", "Cent"),
                new SymbolButton("¥", "\\yen", "Yen"),
                new SymbolButton("§", "\\S", "Section"),
                new SymbolButton("¶", "\\P", "Paragraph"),
                new SymbolButton("•", "\\bullet", "Bullet"),
                new SymbolButton("…", "\\ldots", "Ellipsis"),
            });
            return category;
        }

        /// <summary>
        /// Get all predefined categories
        /// </summary>
        public static List<SymbolCategory> GetAllCategories()
        {
            return new List<SymbolCategory>
            {
                GetGreekLetters(),
                GetOperators(),
                GetRelations(),
                GetLogicSets(),
                GetCalculus(),
                GetFunctions(),
                GetLinearAlgebra(),
                GetChemistry(),
                GetBrackets(),
                GetOtherSymbols(),
            };
        }
    }
}
