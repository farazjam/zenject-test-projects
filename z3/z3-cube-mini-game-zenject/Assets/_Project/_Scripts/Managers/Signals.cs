using Cube.MiniGame.Data;
using Cube.MiniGame.Blocks;
using Cube.MiniGame.Abstract;

namespace Cube.MiniGame.Systems
{
    public class SystemStateChangedSignal
    {
        public SystemState state;
        public SystemStateChangedSignal(SystemState state)
        {
            this.state = state;
        }
    }

    public class LevelConclusionSignal
    {
        public LevelConclusion conclusion;
        public int levelNumber;

        public LevelConclusionSignal(LevelConclusion conclusion, int levelNumber)
        {
            this.conclusion = conclusion;
            this.levelNumber = levelNumber;
        }
    }

    public class BlockTouchedSignal
    {
        public BlockType touchedType;
        public BlockType toucherType;
        public Block toucherBlock;

        public BlockTouchedSignal(BlockType touchedType, BlockType toucherType, Block toucherBlock)
        {
            this.touchedType = touchedType;
            this.toucherType = toucherType;
            this.toucherBlock = toucherBlock;
        }
    }
}