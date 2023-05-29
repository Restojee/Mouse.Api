import { Module } from '@nestjs/common';
import { AuthController } from './auth.controller';
import { LocalStrategy } from './strategies/local.strategy';
import { PassportModule } from '@nestjs/passport';
import { AuthService } from './auth.service';
import { JwtModule } from '@nestjs/jwt';
import { JwtStrategy } from './strategies/jwt.strategy';
@Module({
  imports: [
    PassportModule,
    JwtModule.register({
      secret: 'test',
      signOptions: { expiresIn: '30d' },
    }),
  ],
  controllers: [AuthController],
  providers: [
    AuthService,
    LocalStrategy,
    JwtStrategy
  ],
})
export class AuthModule {}
