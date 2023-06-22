import { Module } from '@nestjs/common';
import { DatabaseModule } from "../../database/database.module";
import { TagService } from "./tag.service";
import { TagRepository } from "./tag.repository";
import { TagEntity } from "../../database/entities/tag.entity";
import { TagController } from "./tag.controller";
import { MapModule } from "../map/map.module";


@Module({
  imports: [
    DatabaseModule,
    MapModule
  ],
  providers: [
    TagService,
    TagRepository,
    {
      provide: 'TAGS_REPOSITORY',
      useValue: TagEntity,
    },
  ],
  controllers: [ TagController ],
  exports: [ TagService ],
})
export class TagModule {}
