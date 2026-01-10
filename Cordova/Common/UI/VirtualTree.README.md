# VirtualTree 사용 가이드

## 개요
VirtualTree는 디자이너에서 생성한 UI 컨트롤들을 트리 구조로 접근할 수 있게 해주는 시스템입니다.
HTML의 DOM 구조와 유사하게, 계층적으로 컨트롤들을 관리하고 접근할 수 있습니다.

## 구조 예시
```
<Root>
  <Row1> (tableLayoutPanel1)
    <Column1>
      <LabelFolderName>
      <LabelPackageName>
      ...
    </Column1>
    <Column2>
      <TextBoxFolderName>
      <TextBoxPackageName>
      ...
    </Column2>
  </Row1>
  <Row2> (tableLayoutPanel2)
    <Column1>...</Column1>
    <Column2>...</Column2>
  </Row2>
  <Row3> (panelPlugins)
    <ButtonBuild>
    <ListBoxPlugins>
    ...
  </Row3>
  <Row4> (panelBuildLog)
    <TextBox1>
  </Row4>
</Root>
```

## 사용 방법

### 1. 초기화
```csharp
private VirtualTree _virtualTree = new VirtualTree();

public void InitWithVirtualTree()
{
    // 노드 추가
    var row1 = _virtualTree.AddNode("Row1", tableLayoutPanel1);
    var col1 = row1.AddChild("Column1", null);
    col1.AddChild("TextBoxFolderName", textBoxFolderName);
}
```

### 2. 경로를 통한 접근
```csharp
// 경로로 노드 찾기
var node = _virtualTree.FindNode("Row1/Column2/TextBoxFolderName");
if (node != null)
{
    var textBox = node.GetControl<DarkTextBox>();
    Console.WriteLine(textBox.Text);
}
```

### 3. 인덱스를 통한 접근
```csharp
// Root의 첫 번째 자식 (Row1)
var row1 = _virtualTree.Root[0];

// Row1의 "Column2" 자식
var col2 = row1["Column2"];
```

### 4. 자식 노드 순회
```csharp
var row = _virtualTree.FindNode("Row1");
row.ForEach(column =>
{
    column.ForEach(control =>
    {
        Console.WriteLine($"{control.Name}: {control.Control?.Text}");
    });
});
```

### 5. Build 메서드 사용
```csharp
_virtualTree.Build(root =>
{
    var pluginRow = root.GetChild("Row3");
    var listBoxNode = pluginRow?.GetChild("ListBoxPlugins");
    var listBox = listBoxNode?.GetControl<ListBox>();
    // listBox 사용
});
```

### 6. 전체 트리 순회
```csharp
_virtualTree.Traverse(node =>
{
    if (node.Control != null)
    {
        Console.WriteLine($"경로: {node.GetPath()}, 타입: {node.Control.GetType().Name}");
    }
});
```

## VirtualNode 주요 메서드

- `AddChild(string name, Control control)` - 자식 노드 추가
- `GetChild(string name)` - 이름으로 자식 노드 찾기
- `GetChild(int index)` - 인덱스로 자식 노드 찾기
- `GetControl<T>()` - 컨트롤을 특정 타입으로 가져오기
- `ForEach(Action<VirtualNode> action)` - 모든 자식 노드 순회
- `GetPath()` - 현재 노드의 전체 경로 가져오기
- `this[string name]` - 인덱서로 자식 접근
- `this[int index]` - 인덱서로 자식 접근

## VirtualTree 주요 메서드

- `AddNode(string name, Control control, VirtualNode parent)` - 노드 추가
- `FindNode(string path)` - 경로로 노드 찾기
- `Build(Action<VirtualNode> builder)` - Root 노드로 빌드 작업 수행
- `Traverse(Action<VirtualNode> action)` - 전체 트리 순회

## 실제 사용 예제

### Build 메서드에서 모든 설정 값 가져오기
```csharp
public void Build()
{
    _virtualTree.Build(root =>
    {
        // Row1의 텍스트 박스들 가져오기
        var row1 = root.GetChild("Row1");
        var col2 = row1?.GetChild("Column2");
        
        var folderName = col2?.GetChild("TextBoxFolderName")?.GetControl<DarkTextBox>()?.Text;
        var packageName = col2?.GetChild("TextBoxPackageName")?.GetControl<TextBox>()?.Text;
        
        // Row2의 콤보박스들 가져오기
        var row2 = root.GetChild("Row2");
        var col2_2 = row2?.GetChild("Column2");
        
        var orientation = col2_2?.GetChild("ComboBoxOrientation")?.GetControl<ComboBox>()?.SelectedItem?.ToString();
        var minSdk = col2_2?.GetChild("ComboBoxMinSdkVersion")?.GetControl<ComboBox>()?.SelectedItem?.ToString();
        
        // 빌드 로직...
    });
}
```

### 특정 Row의 모든 입력값 검증
```csharp
public bool ValidateRow1()
{
    bool isValid = true;
    var row1 = _virtualTree.FindNode("Row1/Column2");
    
    row1?.ForEach(node =>
    {
        if (node.Control is TextBox tb && string.IsNullOrEmpty(tb.Text))
        {
            isValid = false;
        }
    });
    
    return isValid;
}
```

## 장점

1. **계층적 구조**: UI를 논리적인 그룹으로 구조화
2. **유연한 접근**: 경로, 인덱스, 이름 등 다양한 방법으로 접근
3. **순회 편의성**: ForEach, Traverse 등으로 쉽게 순회
4. **타입 안전성**: GetControl<T>()로 타입 안전한 접근
5. **유지보수**: UI 구조를 한 곳에서 관리
