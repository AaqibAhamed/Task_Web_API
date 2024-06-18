import { NgFor } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

interface WeatherForecast {
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

}

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, AppComponent, NgFor],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  public forecasts: WeatherForecast[] = [];

  public taskData: Task[] = [];

  public userData: User[] = [];


  constructor(private http: HttpClient) {

  }
  ngOnInit() {

    this.authenticate();

  }

  ngAfterViewInit() {
    this.getTasks();
  }

  title = 'task-client-app';
  token = "";
 
  authenticate() {
    let url = "https://localhost:7138/api/v1/authentication/authenticate";

    let body = {
      "username": "AaqibWiki",
      "password": "This is a relatively long sentence that acts as my password"
    }

    this.http.post(url, body, { responseType: 'text' }).subscribe(

      (res) => {

        this.token = res;

        console.log(this.token, " res token");


      },
      (error) => {
        console.error(error);
      }
    );
  }

  getTasks() {
    let url = "https://localhost:7138/api/v1/tasks";

    console.log(this.token, " token");

    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': "Bearer " + this.token 
    });
    let options = { headers: headers };

    this.http.get<Task[]>(url, options).subscribe(
      (result) => {
        this.taskData = result;
        console.log(this.taskData, " taskData ")
      },
      (error) => {
        console.error(error);
      }
    );
  }

  selectTask(task: Task) {

  }

  deleteTask(task: Task) {

  }

  toggleTaskCompletion(task: Task) {

  }

}
