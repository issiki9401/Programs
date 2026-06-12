#include <stdio.h>

int main() {
    int t;
    if (scanf("%d", &t) != 1) return 0;
    
    while (t--) {
        int n, m;
        scanf("%d %d", &n, &m);
        
        int q[10000][2], head = 0, tail = n;
        for (int i = 0; i < n; ++i) {
            scanf("%d", &q[i][0]);
            q[i][1] = (i == m);
        }
        
        int time = 0;
        while (head < tail) {
            int curr_p = q[head][0], is_target = q[head][1];
            int has_higher = 0;
            
            for (int i = head + 1; i < tail; ++i) {
                if (q[i][0] > curr_p) {
                    has_higher = 1;
                    break;
                }
            }
            
            if (has_higher) {
                q[tail][0] = curr_p;
                q[tail][1] = is_target;
                tail++;
                head++;
            } else {
                time++;
                head++;
                if (is_target) {
                    printf("%d\n", time);
                    break;
                }
            }
        }
    }
    return 0;
}