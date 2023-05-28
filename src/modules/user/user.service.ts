import { Injectable } from '@nestjs/common';
import { UserRepository } from './user.repository';
import { plainToClass } from "class-transformer";
import {
  CreateUserArgs,
  GetUserByIdArgs,
  GetUserByUsernameAndPasswordArgs,
  GetUserByUsernameArgs
} from "./user.types";
import { User } from "./models/User";

@Injectable()
export class UserService {
  constructor(private usersRepository: UserRepository) {}

  async findUserByUsernameAndPassword(getUserByUsernameAndPasswordArgs: GetUserByUsernameAndPasswordArgs): Promise<User> {
    const { username, password } = getUserByUsernameAndPasswordArgs;
    const user = await this.usersRepository.getUserByUsernameAndPassword({ username, password });
    return plainToClass(User, user);
  }

  async findUserByUsername(getUserByUsernameArgs: GetUserByUsernameArgs): Promise<User> {
    const { username } = getUserByUsernameArgs;
    const user = await this.usersRepository.getUserByUsername({ username });
    return plainToClass(User, user);
  }

  async findUserById(getUserByIdArgs: GetUserByIdArgs): Promise<User> {
    const { userId } = getUserByIdArgs;
    const user = await this.usersRepository.getUserById({ userId });
    return plainToClass(User, user);
  }

  async createUser(createUserArgs: CreateUserArgs): Promise<User> {
    const { username, password } = createUserArgs;
    const user = await this.usersRepository.createUser({ username, password });
    return plainToClass(User, user);
  }
}
