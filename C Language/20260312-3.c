int countBetween0AndZ(int arr[], int n) {
    int count = 0;
    for (int i = 0; i < n; i++) {
        if (arr[i] > '0' && arr[i] < 'Z') {
            count++;
        }
    }
    return count;
}