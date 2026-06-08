# Equation Keyboard System

## Overview

The Equation Keyboard is a comprehensive UI system that provides users with:

- **Symbol Categories**: Organized tabs for different types of symbols
- **Quick Access**: One-click insertion of mathematical and chemical symbols
- **Search Functionality**: Find symbols by name or description
- **Customization**: Add, remove, or modify symbols
- **Presets**: Save and load keyboard configurations

## Components

### 1. SymbolCategory.cs
Defines symbol categories and the symbol library.

**Key Classes:**
- `SymbolCategory` - Represents a category with multiple symbols
- `SymbolButton` - Represents a single insertable symbol
- `SymbolLibrary` - Static library containing all predefined categories

**Predefined Categories:**
- Greek Letters (α, β, γ, ...)
- Operators (+, −, ×, ÷, ±, ...)
- Relations (=, ≠, <, >, ≤, ≥, ...)
- Logic & Set Theory (∀, ∃, ∧, ∨, ...)
- Calculus (∫, ∬, ∂, ∇, ...)
- Functions (sin, cos, tan, log, exp, ...)
- Linear Algebra (⟨⟩, ‖, ·, ×, ...)
- Chemistry (→, ←, ⇌, Δ, ...)
- Brackets ({}, [], (), ⌊⌉, ...)
- Other Symbols (°, ′, ™, ©, ...)

### 2. EquationKeyboardUI.cs
UI component that displays categories and symbols.

**Features:**
- Tab-based category switching
- Grid layout for symbol buttons
- Tooltip support
- Auto-insertion with proper formatting

**Key Methods:**
- `SelectCategory(int index)` - Switch to category
- `InsertSymbol(SymbolButton symbol)` - Insert symbol at cursor
- `InsertFraction()` - Quick insert fraction
- `InsertSquareRoot()` - Quick insert square root
- `InsertSuperscript()` - Quick insert superscript
- `InsertSubscript()` - Quick insert subscript
- `InsertIntegral()` - Quick insert integral
- `InsertMatrix()` - Quick insert matrix

### 3. EquationKeyboardController.cs
Manages keyboard behavior and advanced features.

**Features:**
- Visibility toggle with animation
- Symbol search functionality
- Custom symbol management
- Preset save/load
- Keyboard animation (fade in/out)

**Key Methods:**
- `ToggleKeyboard()` - Show/hide keyboard
- `ShowKeyboard()` - Fade in animation
- `HideKeyboard()` - Fade out animation
- `AddCustomSymbol(SymbolButton symbol)` - Add custom symbol
- `RemoveCustomSymbol(string latexCommand)` - Remove symbol
- `ExportPreset()` - Save configuration
- `ImportPreset(string presetJson)` - Load configuration

## Usage

### Basic Setup

1. **Create UI Canvas**
   - Add a Canvas to your scene
   - Create hierarchy: Canvas → Keyboard → [Tabs] and [Buttons]

2. **Add Keyboard Components**
   ```csharp
   // Add EquationKeyboardUI to keyboard GameObject
   // Assign references:
   // - Target Input: Your TEXInput component
   // - Tab Container: Parent for category tabs
   // - Button Container: Parent for symbol buttons
   ```

3. **Create Prefabs**
   - Create simple Button prefabs for tabs and symbols
   - Assign to UI component

4. **Add Controller (Optional)**
   ```csharp
   // Add EquationKeyboardController for advanced features
   // - Search functionality
   // - Keyboard toggle animation
   // - Custom symbol management
   ```

### Example Scene Setup

```
Canvas
├─ EquationKeyboardUI (Script)
│  └─ SymbolKeyboard (Panel)
│     ├─ TabsContainer (GridLayout)
│     │  └─ [Tab Button Prefabs]
│     └─ ButtonsContainer (GridLayout)
│        └─ [Symbol Button Prefabs]
└─ TEXInput (for receiving symbols)
```

## Symbol Categories

### Greek Letters
```
α (alpha)    β (beta)     γ (gamma)    δ (delta)
ε (epsilon)  ζ (zeta)     η (eta)      θ (theta)
λ (lambda)   π (pi)       ρ (rho)      σ (sigma)
ω (omega)    ... (22 total)
```

### Mathematical Operators
```
+ − × ÷ ± ∓ · * / ^
√ ∛ ∜ | ∣
```

### Relations
```
= ≠ < > ≤ ≥ ≪ ≫ ≈ ≡ ~ ∝
∈ ∉ ⊂ ⊆
```

### Calculus
```
∫ ∬ ∭ ∮ ∂ ∇ ′ ″ ∞
Δ δ lim →
```

### Functions
```
sin cos tan cot sec csc
arcsin arccos arctan
log ln exp
min max
```

### Chemistry
```
→ ← ⇌ ↑ ↓ = # Δ ° • + −
```

## Advanced Features

### Search Functionality

```csharp
// Users can search for symbols by:
// - Label: "pi" → π
// - Tooltip: "Greek pi" → π
// - LaTeX command: "\\pi" → π

// Results limited to 20 most relevant symbols
```

### Custom Symbols

```csharp
// Add custom symbol to keyboard
var controller = GetComponent<EquationKeyboardController>();
controller.AddCustomSymbol(
    new SymbolButton("⊙", "\\odot", "Custom symbol")
);

// Remove custom symbol
controller.RemoveCustomSymbol("\\odot");
```

### Presets

```csharp
// Save keyboard configuration
string preset = controller.ExportPreset();
PlayerPrefs.SetString("KeyboardPreset", preset);

// Load keyboard configuration
string savedPreset = PlayerPrefs.GetString("KeyboardPreset");
controller.ImportPreset(savedPreset);
```

### Animation

```csharp
// Keyboard shows/hides with fade animation
controller.ShowKeyboard();   // Fade in over 0.3s
controller.HideKeyboard();   // Fade out over 0.3s

// Customizable animation duration
// Set m_AnimationDuration in inspector
```

## API Reference

### SymbolButton
```csharp
public class SymbolButton
{
    public string label;              // Display text
    public string latexCommand;       // LaTeX command
    public string tooltip;            // Hover text
    public bool insertBraces;         // Auto-wrap in braces
}
```

### SymbolCategory
```csharp
public class SymbolCategory
{
    public string name;               // Category name
    public string icon;               // Tab icon
    public List<SymbolButton> symbols; // Symbols in category
}
```

### EquationKeyboardUI
```csharp
public void SelectCategory(int index);
public void InsertSymbol(SymbolButton symbol);
public void InsertFraction();
public void InsertSquareRoot();
public void InsertSuperscript();
public void InsertSubscript();
public void InsertIntegral();
public void InsertMatrix();
```

### EquationKeyboardController
```csharp
public void ToggleKeyboard();
public void ShowKeyboard();
public void HideKeyboard();
public void AddCustomSymbol(SymbolButton symbol);
public void RemoveCustomSymbol(string latexCommand);
public string ExportPreset();
public void ImportPreset(string presetJson);
public bool IsVisible { get; }
```

## Customization

### Add New Category

```csharp
// In SymbolLibrary.cs
public static SymbolCategory GetMyCustomCategory()
{
    var category = new SymbolCategory("My Symbols", "✓");
    category.symbols.AddRange(new SymbolButton[]
    {
        new SymbolButton("⊕", "\\oplus", "My symbol"),
        // ...
    });
    return category;
}

// Add to GetAllCategories()
// in the returned list
```

### Modify Symbol Properties

```csharp
// Change display color
var tabImage = tabButton.GetComponent<Image>();
tabImage.color = new Color(0, 1, 0, 1);  // Green

// Change button size
var layoutElement = symbolButton.GetComponent<LayoutElement>();
layoutElement.preferredWidth = 50;
layoutElement.preferredHeight = 50;
```

### Add Keyboard Shortcuts

```csharp
// In Update method
if (Input.GetKeyDown(KeyCode.F1))
    controller.ToggleKeyboard();

if (Input.GetKeyDown(KeyCode.F2))
    keyboardUI.InsertFraction();
```

## Performance Tips

1. **Lazy Load Categories**: Create only visible category buttons
2. **Pool Symbol Buttons**: Reuse button instances
3. **Limit Search Results**: Show only top 20 matches
4. **Cache Symbols**: Pre-filter symbols by type

## Troubleshooting

### Symbols not inserting
- Check TEXInput reference is assigned
- Verify TEXInput has focus
- Check LaTeX command format is correct

### Search not working
- Verify SearchField is assigned
- Check symbol tooltips are populated
- Ensure filtering logic is correct

### Keyboard not appearing
- Check CanvasGroup is on keyboard GameObject
- Verify initial visibility setting
- Check canvas rendering order

## Future Enhancements

- [ ] Custom keyboard layouts
- [ ] Symbol favorites/recent list
- [ ] Voice input for symbol names
- [ ] Symbol groups and submenus
- [ ] Keyboard shortcuts display
- [ ] Symbol preview panel
- [ ] Multi-language support
- [ ] Keyboard themes

## Examples

See `ChemicalEquationExample.cs` for integration examples.
