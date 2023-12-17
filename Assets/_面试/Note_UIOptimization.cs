namespace Questions
{
    public class Note_UIOptimization
    {
        // 1.同一Canvas下的UI元素才能合批。
        // 不同Canvas即使Order in Layer相同也不合批，所以UI的合理规划和制作非常重要。
        
        // 2.尽量整合并制作图集，从而使得不同UI元素的材质图集一致。
        // 图集中的按钮、图标等需要使用图片的比较小的UI元素，完全可以整合并制作图集。当它们密集地同时出现时，就有效降低了DrawCall。
        
        // 3.在同一Canvas下、且材质和图集一致的前提下，避免层级穿插。
        // 笼统地说，应使得符合合批条件的UI元素的“层级深度”相同。
        
        // 4.将相关UI的Pos Z尽量统一设置为0
        // Z值不为0的UI元素只能与Hierarchy中相邻元素尝试合批，所以容易打断合批。
        
        // 5.对于Alpha为0的Image，需要勾选其CanvasRender组件上的Cull Transparent Mesh选项，否则依然会产生DrawCall且容易打断合批。
        
        // 6.Canvas分层
        // 将UI分成多个Canvas，可以减少重绘。比如背景、互动和前景分别使用不同的Canvas。

        // 7.使用Canvas Group
        // 可以控制一组UI元素的显示和隐藏，而不是单个元素，以减少开销。

        // 8.合理使用Raycast Target
        // 对不需要交互的UI元素，关闭其Raycast Target属性，以减少事件系统的计算负担。

        // 9.优化Graphic Raycaster
        // Graphic Raycaster用于处理UI的射线投射，如果UI界面很复杂，它会消耗较多的性能，可以适当地关闭部分不需要的Raycaster。

        // 10.批处理
        // Unity UI默认支持批处理，尽量保持UI元素的材质和图集一致，来减少Draw Calls。

        // 11.避免Layout组件的频繁重排
        // 如GridLayoutGroup、VerticalLayoutGroup等，在不需要动态调整布局时，可以关闭或移除这些组件。
    }
}