# File descriptions

## Unity /Assets/Scripts
- **Board2D.cs** – board state (bitboards), FEN IO, sprite sync.
- **Move.cs** – move struct/flags.
- **MoveGen.cs** – (placeholder) move generation; later: sliders, pawns, castles, EP.
- **Rules.cs** – (placeholder) legality filter, termination conditions.
- **GameManager.cs** – game loop and glue.
- **TouchController.cs** – mobile touch input.
- **EnvBridge.cs** – (placeholder) socket bridge to Python policy.
- **ObservationEncoder.cs** – board -> tensor/planes.
- **ActionDecoder.cs** – flat index -> move struct.
- **UI/HUD.cs** – simple labels for iterations/score.

## Unity other
- **Assets/Sprites/** – import artwork here.
- **Assets/Scenes/Chess2D.unity** – scene placeholder (Unity will create proper file).
- **ProjectSettings/**, **Packages/** – Unity config (populated by Unity Editor).

## Python
- **models.py** – policy/value networks.
- **ppo.py** – PPO trainer loop.
- **env_client.py** – reference client/server for action selection.
- **train.py** – training controller & rollouts.
- **requirements.txt** – pinned deps.
