# 🎮 Unity Figma-Style App with Coin Tap Game

A modular and scalable Unity Android application that simulates a flow with clean UI transitions, login validation, and an engaging coin-tap mini-game. Built within a 24-hour timeframe, this project serves as a solid foundation for larger, feature-rich apps.

---

## 🚀 Features

### 🔐 Entry & Login Flow
- **Entry Screen**: One-tap login entry with smooth transition to loading screen.
- **Reusable Loading Screen**: Coroutine-based fake loading bar with fade-in/out effects.
- **Login Screen**:
  - Phone number input (10-digit validation).
  - Password input with hide/unhide toggle.
  - Inline validation and user feedback.
  - Simulated login transition.

### 🏠 Main Menu
- **Play Game**: Navigates to Coin Tap mini-game.
- **Exit App**: Clean application exit handling.

---

## 🎮 Coin Tap Mini-Game

- **Objective**: Tap as many coins as possible in 30 seconds!
- **Gameplay**:
  - Coins spawn every 1–2 seconds at random positions.
  - Tapping a coin increases your score and plays an SFX.
- **UI Elements**:
  - Score Counter
  - Countdown Timer
  - Restart & Back to Menu buttons
- **Polish**:
  - Smooth animations (scale/fade) for coins.
  - Button click feedback with scale animations.
  - Pause menu with swipe-to-exit and confirmation popup.

---

## 🎧 Audio

- **Background Music**:
  - Smooth scene-based transitions with fade in/out.
- **Sound Effects**:
  - Coin collection
  - Button presses

---

## ⚙️ Technical Stack

- **Unity Version**: 6000.0.43f
- **UI Framework**: TextMeshPro
- **Scene Management**: `SceneManager.LoadSceneAsync`
- **Architecture**:
  - `GameManager`, `UIManager`, `SceneLoader` separated by responsibilities.
  - Prefabs used for UI coins.
- **Best Practices**:
  - Modular, commented code
  - Coroutine-based transitions
  - Object pooling ready

---

## 📱 Platform

- **Target**: Android (APK included)
- **Input Handling**: Android back button and swipe-to-exit supported.

---

## 📂 Project Structure

```plaintext
_Assets/
├── _Audio/
├── _Prefabs/
├── _Scenes/
├── _Scripts/
│   ├── _Managers/
│   ├── _UI/
├── _textures/
