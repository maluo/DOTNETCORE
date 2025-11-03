import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  csvData: any[] = [];
  headers: string[] = [];
  error: string = '';
  title = 'Ski Playground';

  onFileSelected(event: any): void {
    const file: File = event.target.files[0];

    if (!file) return;

    if (!file.name.endsWith('.csv')) {
      this.error = 'Please select a CSV file';
      return;
    }

    this.parseCsv(file);
  }

  private parseCsv(file: File): void {
    const reader = new FileReader();

    reader.onload = (e: any) => {
      try {
        const csvText = e.target.result;
        this.processCsvData(csvText);
      } catch (error) {
        this.error = 'Error parsing CSV file';
      }
    };

    reader.onerror = () => {
      this.error = 'Error reading file';
    };

    reader.readAsText(file);
  }

  private processCsvData(csvText: string): void {
    const lines = csvText.split('\n').filter(line => line.trim() !== '');

    if (lines.length === 0) {
      this.error = 'CSV file is empty';
      return;
    }

    // Parse headers
    this.headers = this.parseCsvLine(lines[0]);

    // Parse data rows
    this.csvData = [];
    for (let i = 1; i < lines.length; i++) {
      const values = this.parseCsvLine(lines[i]);
      const row: any = {};

      this.headers.forEach((header, index) => {
        row[header] = values[index] || '';
      });

      this.csvData.push(row);
    }

    this.error = '';
  }

  private parseCsvLine(line: string): string[] {
    console.log('Parsing line:', line);
    const result: string[] = [];
    let current = ''; //current words
    let inQuotes = false;
    let nextChar = '';
    let char = '';

    for (let i = 0; i < line.length; i++) {
      char = line[i];
      nextChar = (i + 1 < line.length)? line[i + 1]: '';
      switch(char){
        case '"':
          if (inQuotes && nextChar === '"'){
            // " ""A"" "
            current += '"';
            //now points to next ", and for loop will point to next char by default
            i++;
          }else{
            inQuotes = !inQuotes;
          }
          break;
        case ',':
          if (!inQuotes){
            result.push(current);
            current = '';
          }else{
            current += char;
          }
          break;
        default:
          current += char;
      }
    }

    result.push(current);
    return result.map(field => field.trim().replace(/^"|"$/g, ''));
  }
}
