type PerformanceMetric = {
  name: string;
  value: number;
  delta: number;
  id: string;
  entries: PerformanceEntry[];
};

type PerformanceReportHandler = (metric: PerformanceMetric) => void;

const reportWebVitals = (onPerfEntry?: PerformanceReportHandler): void => {
  if (onPerfEntry && onPerfEntry instanceof Function) {
    import('web-vitals').then(({ onCLS, onFID, onFCP, onLCP, onTTFB }) => {
      onCLS(onPerfEntry);
      onFID(onPerfEntry);
      onFCP(onPerfEntry);
      onLCP(onPerfEntry);
      onTTFB(onPerfEntry);
    });
  }
};

export default reportWebVitals;
