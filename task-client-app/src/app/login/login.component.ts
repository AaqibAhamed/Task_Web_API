import { Component } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { Token } from '../models/token.model';
import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  username: string = '';
  password: string= '';

  private readonly  router: any 

  constructor(private authenticationService: AuthenticationService, private route: RouterModule) {
    route = this.router
   }

  login(): void {
    this.authenticationService.login(this.username, this.password).subscribe((token: Token) => {
      localStorage.setItem('token', token.token);
      this.router.navigate(['/tasks']);
    });
  }
}
