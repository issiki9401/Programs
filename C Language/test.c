int main() {
  int m=3, n=5;
  int a[5]={-4,-3,-2,-1};
  
  int*p=&m;
  int*q=&n;
  *p=7;
  
  int**pp=&p;
  int**pq=&q;
  p=q;
  **pp=1;
  
  p=a;
  q=a+2;
  *(p+2)=*(q+2);
  *q=*p**p;
  
  return 0
}