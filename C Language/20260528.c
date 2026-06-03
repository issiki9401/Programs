#include <stdio.h>
#include <stdbool.h>

// 定義 Box 結構（模擬私有資料成員）
typedef struct {
    double length;
    double breadth;
    double height;
    double volume;
    double surfaceArea;
} Box;

// 內部輔助函式：計算體積與表面積
void calculateMetrics(Box* b) {
    b->volume = b->length * b->breadth * b->height;
    b->surfaceArea = 2 * (b->length * b->breadth + b->breadth * b->height + b->height * b->length);
}

// 模擬預設建構子
void initBoxDefault(Box* b) {
    b->length = 1.0;
    b->breadth = 1.0;
    b->height = 1.0;
    calculateMetrics(b);
}

// 模擬有參數建構子
void initBox(Box* b, double l, double w, double h) {
    b->length = l;
    b->breadth = w;
    b->height = h;
    calculateMetrics(b);
}

// 模擬公有函式成員
double getVolume(const Box* b) {
    return b->volume;
}

double getSurfaceArea(const Box* b) {
    return b->surfaceArea;
}

bool isValid(const Box* b) {
    return (b->length > 0 && b->breadth > 0 && b->height > 0);
}

bool isCube(const Box* b) {
    return (b->length == b->breadth) && (b->breadth == b->height);
}

void printBox(const Box* b) {
    printf("[%g, %g, %g]", b->length, b->breadth, b->height);
}

bool canContain(const Box* thisBox, const Box* otherBox) {
    return (thisBox->length > otherBox->length && 
            thisBox->breadth > otherBox->breadth && 
            thisBox->height > otherBox->height);
}

int main() {
    // 1. 宣告並初始化三個 Box (模擬有參數建構子)
    Box box1, box2, box3;
    initBox(&box1, 5.0, 6.0, 7.0);
    initBox(&box2, 2.0, 2.0, 2.0);
    initBox(&box3, 3.1, 5.23, 2.5);

    // 2. 分別呼叫函式，印出兩個 Box 的相關資訊
    Box* testBoxes[] = {&box1, &box2};
    for (int i = 0; i < 2; ++i) {
        printf("Box %d 資訊:\n尺寸: ", i + 1);
        printBox(testBoxes[i]);
        printf("\n體積: %g\n", getVolume(testBoxes[i]));
        printf("表面積: %g\n", getSurfaceArea(testBoxes[i]));
        printf("是否合法 (isValid): %s\n", isValid(testBoxes[i]) ? "true" : "false");
        printf("是否為正方體 (isCube): %s\n\n", isCube(testBoxes[i]) ? "true" : "false");
    }

    // 3. 判斷第一個 Box 能否裝入第二個 Box，並印出結果
    printf("第一個 Box 是否能裝入第二個 Box: %s\n\n", canContain(&box1, &box2) ? "true" : "false");

    // 4. 印出三個 Box 之中表面積最大者
    Box maxBox = box1;
    if (getSurfaceArea(&box2) > getSurfaceArea(&maxBox)) maxBox = box2;
    if (getSurfaceArea(&box3) > getSurfaceArea(&maxBox)) maxBox = box3;

    printf("表面積最大的 Box 為: ");
    printBox(&maxBox);
    printf("\n");

    return 0;
}