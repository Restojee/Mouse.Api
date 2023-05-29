import { Module } from '@nestjs/common';
import { SequelizeModule } from '@nestjs/sequelize';
import { ConfigModule, ConfigService } from "@nestjs/config";
import { UserEntity } from "./entities/user.entity";
import { MapEntity } from "./entities/map.entity";
import { TagEntity } from "./entities/tag.entity";
import { CommentEntity } from "./entities/comment.entity";
import { MapTagEntity } from "./entities/map-tag.entity";
import { FavoriteEntity } from "./entities/favorite.entity";
import { CompletedEntity } from "./entities/completed.entity";

@Module({
  imports: [
    SequelizeModule.forRootAsync({
      imports: [ConfigModule],
      inject: [ConfigService],
      useFactory: (configService: ConfigService) => {
        return {
          synchronize: true,
          autoLoadModels: true,
          dialect: configService.get("DATABASE_DIALECT"),
          host: configService.get("DATABASE_HOST"),
          port: configService.get("DATABASE_PORT"),
          username: configService.get("DATABASE_USERNAME"),
          password: configService.get("DATABASE_PASSWORD"),
          database: configService.get("DATABASE_NAME"),
          models: [
            UserEntity,
            MapEntity,
            TagEntity,
            CommentEntity,
            MapTagEntity,
            FavoriteEntity,
            CompletedEntity,
          ],
        }
      },
    })
  ],
  exports: [
    SequelizeModule
  ]
})
export class DatabaseModule {}
