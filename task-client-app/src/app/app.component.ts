import { Component, OnInit } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { AuthenticationService } from './services/authentication.service';

/* interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

interface User {
  UserName: string;
  Password: string;
}

interface Task {
  id: string;
  title: string;
  description: string;
  isCompleted: boolean;

} */

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ AppComponent,RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent implements OnInit {

  title = 'Task Management App';
  private readonly router: any

  constructor(private authenticationService: AuthenticationService) {

  }

  ngOnInit(): void {
    if (!this.authenticationService.isAuthenticated()) {
      this.router.navigate(['/login']);
    }
  }

  /* public forecasts: WeatherForecast[] = [];
 
 public taskData: Task[] = [];
 
 public userData: User[] = []; */


  /* title = 'task-client-app';
  token = "";
  url ="https://localhost:7138/api/v1/"
 
  authenticate() {
    let endPoint = "authentication/authenticate";
 
    let body = {
      "username": "AaqibWiki",
      "password": "This is a relatively long sentence that acts as my password"
    }
 
    this.http.post(this.url + endPoint, body, { responseType: 'text' }).subscribe(
 
      (res) => {
 
        this.token = res;
 
        console.log(this.token, " res token");
 
        this.getTasks(this.token);
 
 
      },
      (error) => {
        console.error(error);
      }
    );
  }
 
  getTasks(tok:any) {
    let endPoint = "tasks";
 
    console.log(tok, " token");
 
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': "Bearer " + tok
    });
    let options = { headers: headers };
 
    this.http.get<Task[]>(this.url + endPoint, options).subscribe(
      (result) => {
        this.taskData = result;
        console.log(this.taskData, " taskData ")
      },
      (error) => {
        console.error(error);
      }
    );
  }
 
  */

}
