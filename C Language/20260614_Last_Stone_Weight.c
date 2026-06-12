#include <stdio.h>
#include <stdlib.h>

void swap(int *a, int *b) {
    int temp = *a; *a = *b; *b = temp;
}

void heapify(int arr[], int n, int i) {
    int largest = i, l = 2 * i + 1, r = 2 * i + 2;
    if (l < n && arr[l] > arr[largest]) largest = l;
    if (r < n && arr[r] > arr[largest]) largest = r;
    if (largest != i) {
        swap(&arr[i], &arr[largest]);
        heapify(arr, n, largest);
    }
}

void push(int arr[], int *n, int val) {
    arr[(*n)++] = val;
    for (int i = *n - 1; i > 0 && arr[i] > arr[(i - 1) / 2]; i = (i - 1) / 2)
        swap(&arr[i], &arr[(i - 1) / 2]);
}

int pop(int arr[], int *n) {
    int max = arr[0];
    arr[0] = arr[--(*n)];
    heapify(arr, *n, 0);
    return max;
}

int main() {
    int n;
    while (scanf("%d", &n) == 1) {
        int stones[1005], size = 0;
        for (int i = 0, val; i < n; ++i) {
            scanf("%d", &val);
            push(stones, &size, val);
        }
        while (size > 1) {
            int y = pop(stones, &size);
            int x = pop(stones, &size);
            if (x != y) push(stones, &size, y - x);
        }
        printf("%d\n", size ? stones[0] : 0);
    }
    return 0;
}