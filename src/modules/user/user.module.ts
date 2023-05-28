import { Module } from '@nestjs/common';
import { UserService } from './user.service';
import { UserRepository } from './user.repository';
import { DatabaseModule } from "../../database/database.module";
import { UserEntity } from "../../database/entities/user.entity";

@Module({
  imports: [
    DatabaseModule
  ],
  providers: [
    UserService,
    UserRepository,
    {
      provide: "USERS_REPOSITORY",
      useValue: UserEntity,
    }
  ],
  exports: [UserService],
})
export class UserModule {}
