namespace Yang.DesignMode.Command
{
    public interface ICommand
    {
        // 重做 Y
        void Redo();

        // 撤销 Z
        void Undo();
    }
}