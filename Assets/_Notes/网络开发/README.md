### 常见报错
1. `socket.Available = 0`<br>
可能是有一个自定义消息类对象是 `null` 的，在 `send/receive` 的时候阻塞了

2. 解析自定义类信息反序列化越界<br>
可能是在序列化写入的时候 `Copy` 的位置索引错了

3. `ConnectionReset` 远程主机强迫关闭了一个现有的连接<br>
可能是 `socket=null` 的时候，被 `lock` 了
