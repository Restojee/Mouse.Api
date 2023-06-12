import { Body, Controller, Get, Post, Request, UseGuards } from '@nestjs/common';
import { JwtAuthGuard } from './guards/jwt-auth.guard';
import { AuthService } from './auth.service';
import { RegisterRequest } from './models/RegisterRequest';
import { LoginRequest } from './models/LoginRequest';
import { ApiBearerAuth, ApiOperation, ApiTags } from '@nestjs/swagger';

@ApiTags('Auth')
@Controller('auth')
@ApiBearerAuth()
export class AuthController {
  constructor(private authService: AuthService) {}

  @Post('Login')
  @ApiOperation({ operationId: 'Login' })
  async login(@Body() loginRequest: LoginRequest) {
    const { username, password } = loginRequest;
    return await this.authService.login({ username, password });
  }

  @Post('Register')
  @ApiOperation({ operationId: 'Register' })
  async register(@Body() registerRequest: RegisterRequest) {
    const { password, username } = registerRequest;
    return await this.authService.register({ password, username });
  }

  @UseGuards(JwtAuthGuard)
  @Get('GetProfile')
  @ApiOperation({ operationId: 'GetProfile' })
  async getProfile(@Request() request) {
    const { id } = request.user;
    return await this.authService.getProfile({ userId: id });
  }
}
