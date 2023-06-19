import {
  Inject,
  Injectable
} from '@nestjs/common';
import { Repository } from "sequelize-typescript";
import { CreateUserRequest } from "./models/create-user-request";
import { UserEntity } from "../../database/entities/user.entity";
import {
  GetUserByIdArgs,
  GetUserByUsernameAndPasswordArgs,
  GetUserByUsernameArgs
} from "./user.types";

@Injectable()
export class UserRepository {
  constructor(@Inject('USERS_REPOSITORY') private repository: Repository<UserEntity>) {}

  async getUserByUsernameAndPassword(getUserByUsernameAndPasswordArgs: GetUserByUsernameAndPasswordArgs): Promise<UserEntity> {
    const { username, password } = getUserByUsernameAndPasswordArgs;
    return this.repository.findOne({ where: { username, password } });
  }

  async getUserById(getUserByIdArgs: GetUserByIdArgs): Promise<UserEntity> {
    const { userId } = getUserByIdArgs;
    return this.repository.findOne({ where: { id: userId } });
  }

  async getUserByUsername(getUserByUsernameArgs: GetUserByUsernameArgs): Promise<UserEntity> {
    const { username } = getUserByUsernameArgs;
    return this.repository.findOne({ where: { username } });
  }

  async createUser (createUserRequest: CreateUserRequest): Promise<UserEntity> {
    const { password, username } = createUserRequest;
    return this.repository.create({ password, username });
  }
}
