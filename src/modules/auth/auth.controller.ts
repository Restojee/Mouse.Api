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
  async login(@Body() payload: LoginRequest) {
    return await this.authService.login(payload);
  }

  @Post('Register')
  @ApiOperation({ operationId: 'Register' })
  async register(@Body() payload: RegisterRequest) {
    return await this.authService.register(payload);
  }

  @UseGuards(JwtAuthGuard)
  @Get('GetProfile')
  @ApiOperation({ operationId: 'GetProfile' })
  async getProfile(@Request() request) {
    return await this.authService.getProfile(request.user);
  }
}
