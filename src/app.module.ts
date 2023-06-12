import { Module } from '@nestjs/common';
import { UserModule } from "./modules/user/user.module";
import { ConfigModule } from "@nestjs/config";
import { DatabaseModule } from "./database/database.module";
import { AuthModule } from "./modules/auth/auth.module";
import { MapModule } from "./modules/map/map.module";
import { MediaModule } from "./modules/media/media.module";

@Module({
  imports: [
    DatabaseModule,
    ConfigModule.forRoot({
      envFilePath: `.env.development`,
    }),
    UserModule,
    MediaModule,
    AuthModule,
    MapModule
  ],
  controllers: [],
  providers: [],
})
export class AppModule {}
