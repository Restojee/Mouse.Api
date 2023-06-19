import { Strategy } from 'passport-local';
import { Injectable, UnauthorizedException } from '@nestjs/common';
import { AuthService } from '../auth.service';
import { PassportStrategy } from '@nestjs/passport';
import { LoginRequest } from '../models/login-request';

@Injectable()
export class LocalStrategy extends PassportStrategy(Strategy) {
  constructor(private authService: AuthService) {
    super();
  }

  async validate(authRequest: LoginRequest): Promise<any> {
    const user = await this.authService.login(authRequest);
    if (!user) {
      throw new UnauthorizedException();
    }
    return user;
  }
}
