int areAllOdd(int arr[], int n) {
    for (int i = 0; i < n; i++) {
        if (arr[i] % 2 == 0) {
            return 0;
        }
    }
    return 1;
}