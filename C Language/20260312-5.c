int indexOfSecondLargest(int arr[], int n) {
    if (n < 2) return -1;

    int max1_idx = -1;
    int max2_idx = -1;

    for (int i = 0; i < n; i++) {
        if (max1_idx == -1 || arr[i] > arr[max1_idx]) {
            max2_idx = max1_idx;
            max1_idx = i;
        } else if (arr[i] < arr[max1_idx]) {
            if (max2_idx == -1 || arr[i] > arr[max2_idx]) {
                max2_idx = i;
            }
        }
    }
    return max2_idx;
}