import {
  Injectable,
  BadRequestException,
  UnauthorizedException
} from '@nestjs/common';
import { LoginRequest } from './models/LoginRequest';
import { RegisterRequest } from './models/RegisterRequest';
import { Authorized } from './models/Authorized';
import { JwtService } from '@nestjs/jwt';
import {
  GenerateJwtTokenArgs,
  GetProfileArgs
} from "./auth.types";
import { UserService } from "../user/user.service";

@Injectable()
export class AuthService {
  constructor(private usersService: UserService, private jwtService: JwtService) {}

  async login(loginRequest: LoginRequest): Promise<Authorized> {
    const { username, password } = loginRequest;

    const user = await this.usersService.findUserByUsernameAndPassword({ username, password });

    if (user) {
      return new Authorized(this.generateJwtToken({ id: user.id, username }), user)
    }

    throw new BadRequestException(`Неправильный логин или пароль`);
  }

  async register(registerRequest: RegisterRequest): Promise<Authorized> {
    const { username, password} = registerRequest;

    if (await this.usersService.findUserByUsername({ username })) {
      throw new BadRequestException(`Логин ${username} уже занят`);
    }

    const user = await this.usersService.createUser({ username, password });

    if (user) {
      return new Authorized(this.generateJwtToken({ username, id: user.id }), user)
    }

    throw new BadRequestException(`Произошла неопознанная ошибка при регистрации, попробуйте снова`);
  }

  async getProfile(getProfileArgs: GetProfileArgs) {
    const { userId } = getProfileArgs;

    const foundUser = await this.usersService.findUserById({ userId });

    if (!foundUser) {
      throw new UnauthorizedException();
    }

    return foundUser;
  }

  generateJwtToken(generateJwtTokenArgs: GenerateJwtTokenArgs) {
    const { id, username } = generateJwtTokenArgs;
    return this.jwtService.sign({ id, username });
  }
}
