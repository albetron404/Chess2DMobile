# Chess2DMobile – Unity 2D Chess RL

A **mobile-ready Unity project** scaffold for experimenting with **Reinforcement Learning (RL)** in chess.
This repository combines a **Unity 2D front-end** (board, pieces, UI, touch input) with a **Python backend** for training (e.g. PPO).
The goal is to simulate agent-vs-agent play, support human play on mobile, and enable training loops for AI.

---

## 📂 Repository Structure

```
Chess2DMobile/
  unity/                # Unity project root
    Assets/
      Scripts/          # C# scripts (Board, Rules, RL bridge, UI)
      Sprites/          # Chessboard & piece assets (placeholders)
      Scenes/           # Unity scenes (Chess2D.unity)
      UI/               # HUD and UI overlays
    ProjectSettings/    # Unity project config
    Packages/           # Unity package config
  python/               # Python training loop
    models.py           # Policy/value networks
    ppo.py              # PPO trainer
    env_client.py       # Socket/gRPC bridge to Unity
    train.py            # Training orchestration
    requirements.txt    # Python dependencies
  docs/                 # Extra docs (file descriptions, dev notes)
```

---

## 🚀 Features (current & planned)

* **Mobile-friendly Unity setup** (2D, touch input, lightweight UI).
* **Chess board representation** with placeholder scripts (Board, Move, Rules).
* **Scene placeholder** (`Chess2D.unity`) ready to wire with prefabs.
* **Python scaffold** for RL training (PPO, Unity bridge).
* **Cross-platform** target: Android & iOS.
* Git-based workflow with `.gitignore` tuned for Unity + Python.

---

## 🛠 Getting Started

1. **Clone the repo:**

   ```bash
   git clone <YOUR_REMOTE_URL>
   cd Chess2DMobile
   ```

2. **Unity side:**

   * Open the `unity/` folder in Unity Hub (2D project).
   * Import sprites and replace placeholders in `Assets/Sprites/`.
   * Use the provided scripts as starting points for board + game logic.

3. **Python side:**

   * Install dependencies:

     ```bash
     cd python
     pip install -r requirements.txt
     ```
   * Extend `env_client.py` and `train.py` for agent training.

---

## 📱 Target

* **Unity 2D Mobile** (Android/iOS build).
* **Self-play RL** with Python PPO backend.
* **Optional human vs AI play** with simple HUD.

---

## 🤝 Contributing

* Extend chess rules & move generation (`MoveGen.cs`, `Rules.cs`).
* Improve UI/UX for mobile (HUD, menus, animations).
* Implement and test PPO training loop.
* Add sprites and prefabs for production-ready assets.

---
